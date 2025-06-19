using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CenterDragon.Data
{
    public class ApplicationUser:IdentityUser
    {
    
        public string FullName { get; set; }

        public int Age { get; set; }
        public string Ediation { get; set; }
        public string Adress
        {
            get; set;
        }
    }
}
