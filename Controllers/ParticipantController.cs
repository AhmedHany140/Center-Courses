using AutoMapper;
using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Models.Entites;
using CenterDragon.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CenterDragon.Controllers
{
	/// <summary>
	/// Controller responsible for managing participants and their related operations.
	/// </summary>
	public class ParticipantController : Controller
	{
		private readonly IParticipantRepository _participantRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParticipantController"/> class.
		/// </summary>
		/// <param name="participantRepository">The participant repository.</param>
		/// <param name="userManager">The user manager for application users.</param>
		/// <param name="mapper">The AutoMapper instance.</param>
		public ParticipantController(
			IParticipantRepository participantRepository,
			UserManager<ApplicationUser> userManager,
			IMapper mapper)
		{
			_participantRepository = participantRepository;
			_userManager = userManager;
			_mapper = mapper;
		}

		/// <summary>
		/// Displays a list of all participants.
		/// </summary>
		/// <returns>The Index view with the list of participants.</returns>
		public async Task<IActionResult> Index()
		{
			var participants = _participantRepository.GetAll();
			return View(participants);
		}

		/// <summary>
		/// Displays the form to add a new participant.
		/// </summary>
		/// <returns>The Add view with the participant view model.</returns>
		public async Task<IActionResult> Add()
		{
			var viewModel = new participantViewModel
			{
				Courselist = _participantRepository.AllCourses() ?? new List<Course>()
			};
			return View(viewModel);
		}

		/// <summary>
		/// Saves a new participant to the database.
		/// </summary>
		/// <param name="viewModel">The participant view model containing participant data.</param>
		/// <returns>Redirects to Index on success, otherwise returns the Add view with validation errors.</returns>
		[HttpPost]
		public async Task<IActionResult> SaveData(participantViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Courselist = _participantRepository.AllCourses() ?? new List<Course>();
				return View("Add", viewModel);
			}

			if (viewModel.ParCourseId == 0)
			{
				viewModel.Courselist = _participantRepository.AllCourses() ?? new List<Course>();
				ModelState.AddModelError("ParCourseId", "Choose a Course");
				return View("Add", viewModel);
			}

			var participant = _mapper.Map<Participants>(viewModel);

			// Get instructor ID and assign
			int? instructorId = _participantRepository.GetInstractorId(viewModel.ParCourseId);
			if (instructorId.HasValue)
			{
				participant.InstractorId = instructorId.Value;
			}

			_participantRepository.Add(participant);
			_participantRepository.Save();

			await AssignRoleBasedOnCourse(participant);

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Deletes a participant by their ID.
		/// </summary>
		/// <param name="id">The participant's ID.</param>
		/// <returns>Redirects to Index after deletion.</returns>
		public async Task<IActionResult> Delete(int id)
		{
			_participantRepository.DeletebyId(id);
			_participantRepository.Save();
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Displays the details of a participant.
		/// </summary>
		/// <param name="id">The participant's ID.</param>
		/// <returns>The Details view with participant information.</returns>
		public async Task<IActionResult> Details(int id)
		{
			var participant = _participantRepository.GetById(id);
			return View(participant);
		}

		/// <summary>
		/// Displays the form to edit a participant.
		/// </summary>
		/// <param name="id">The participant's ID.</param>
		/// <returns>The Edit view with the participant view model.</returns>
		public async Task<IActionResult> Edit(int id)
		{
			var participant = _participantRepository.GetById(id);
			var viewModel = _mapper.Map<participantViewModel>(participant);
			viewModel.Courselist = _participantRepository.AllCourses() ?? new List<Course>();
			return View(viewModel);
		}

		/// <summary>
		/// Saves the edited participant data to the database.
		/// </summary>
		/// <param name="viewModel">The participant view model with updated data.</param>
		/// <returns>Redirects to Index on success, otherwise returns the Edit view with validation errors.</returns>
		[HttpPost]
		public async Task<IActionResult> SaveEdite(participantViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Courselist = _participantRepository.AllCourses() ?? new List<Course>();
				return View("Edit", viewModel);
			}

			if (viewModel.ParCourseId == 0)
			{
				viewModel.Courselist = _participantRepository.AllCourses() ?? new List<Course>();
				ModelState.AddModelError("ParCourseId", "Choose a Course");
				return View("Edit", viewModel);
			}

			var participant = _mapper.Map<Participants>(viewModel);
			_participantRepository.Edit(participant);
			_participantRepository.Save();

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Assigns a role to the user based on the participant's course.
		/// </summary>
		/// <param name="participant">The participant entity.</param>
		private async Task AssignRoleBasedOnCourse(Participants participant)
		{
			var students = _participantRepository.AllStudents();
			var securityKey = students.FirstOrDefault(s => s.Email == participant.Email)?.SecurtyKey;

			if (string.IsNullOrEmpty(securityKey)) return;

			var user = await _userManager.FindByIdAsync(securityKey);
			if (user == null) return;

			string roleName = participant.CourseId switch
			{
				1 => "BackEnd",
				2 => "FrontEnd",
				3 => "FullStack",
				_ => null
			};

			if (!string.IsNullOrEmpty(roleName))
			{
				var result = await _userManager.AddToRoleAsync(user, roleName);
				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
		}
	}
}