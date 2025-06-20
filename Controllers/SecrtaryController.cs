using Microsoft.AspNetCore.Mvc;

namespace CenterDragon.Controllers
{
	/// <summary>
	/// Controller responsible for handling secretary-related pages and actions.
	/// </summary>
	public class SecrtaryController : Controller
	{
		/// <summary>
		/// Displays the main page for the secretary.
		/// </summary>
		/// <returns>The Index view.</returns>
		public IActionResult Index()
		{
			return View();
		}
	}
}
