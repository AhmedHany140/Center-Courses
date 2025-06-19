using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Reposatories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CenterDragon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Configure Identity to use ApplicationUser and IdentityRole
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IStudent, ParticipantReposatory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Add Authentication and Authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Map Razor Pages
            app.MapRazorPages();

            // Route configuration to ensure login is shown first for unauthenticated users
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        
            InitializeRolesAndUsers(app);

            app.Run();
        }

        private static void InitializeRolesAndUsers(WebApplication app)
        {
            using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Task.Run(async () =>
            {
               
                string[] roleNames = { "BackEnd", "FrontEnd", "FullStack", "student" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

     
                var studentUser = await userManager.FindByEmailAsync("student@example.com");
                if (studentUser == null)
                {
                    studentUser = new ApplicationUser
                    {
                        UserName = "student@example.com",
                        Email = "student@example.com",
                        FullName = "Student User",
                        Adress = "456 Student Street",
                        Age = 18,
                        Ediation = "Bachelor's Degree"
                    };
                    var result = await userManager.CreateAsync(studentUser, "Student@1234");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(studentUser, "student");
                    }
                }
            }).Wait();
        }
    }
}
