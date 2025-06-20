using Microsoft.AspNetCore.Mvc;

namespace CenterDragon.Controllers
{
	/// <summary>
	/// Controller responsible for handling course-related pages and views.
	/// </summary>
	public class CourseController : Controller
	{
		/// <summary>
		/// Displays the FrontEnd course view.
		/// </summary>
		/// <returns>The FrontEnd course view.</returns>
		public IActionResult FrontEnd()
		{
			return View();
		}

		/// <summary>
		/// Displays the BackEnd course view.
		/// </summary>
		/// <returns>The BackEnd course view.</returns>
		public IActionResult BackEndEnd()
		{
			return View();
		}

		/// <summary>
		/// Displays the FullStack course view.
		/// </summary>
		/// <returns>The FullStack course view.</returns>
		public IActionResult FullStack()
		{
			return View();
		}

		/// <summary>
		/// Displays the general course page view.
		/// </summary>
		/// <returns>The course page view.</returns>
		public IActionResult CoursePage()
		{
			return View();
		}

		/// <summary>
		/// Displays the Frontpage view for courses.
		/// </summary>
		/// <returns>The Frontpage view.</returns>
		public IActionResult Frontpage()
		{
			return View();
		}

		/// <summary>
		/// Displays the Fullstack page view for courses.
		/// </summary>
		/// <returns>The Fullstack page view.</returns>
		public IActionResult Fullstackpage()
		{
			return View();
		}
	}
}
