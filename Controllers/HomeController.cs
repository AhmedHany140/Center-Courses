using CenterDragon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CenterDragon.Controllers
{
	/// <summary>
	/// Controller responsible for handling home and message-related pages for students.
	/// </summary>
	[Authorize(Roles = "student")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="HomeController"/> class.
		/// </summary>
		/// <param name="logger">The logger instance.</param>
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Displays the home page.
		/// </summary>
		/// <returns>The Index view.</returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Displays the message page for students.
		/// </summary>
		/// <returns>The MessagePage view.</returns>
		public IActionResult MessagePage()
		{
			return View();
		}

		/// <summary>
		/// Displays the error page with request details.
		/// </summary>
		/// <returns>The Error view with error information.</returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
