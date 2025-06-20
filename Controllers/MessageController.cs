using AutoMapper;
using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Models.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsible for handling messaging operations between students and secretaries.
/// </summary>
public class MessageController : Controller
{
	private readonly IMessageRepository _messageRepository;
	private readonly IStudentRepository _studentRepository;
	private readonly ISecretaryRepository _secretaryRepository;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initializes a new instance of the <see cref="MessageController"/> class.
	/// </summary>
	/// <param name="messageRepository">The message repository.</param>
	/// <param name="studentRepository">The student repository.</param>
	/// <param name="secretaryRepository">The secretary repository.</param>
	/// <param name="userManager">The user manager for application users.</param>
	/// <param name="mapper">The AutoMapper instance.</param>
	public MessageController(
		IMessageRepository messageRepository,
		IStudentRepository studentRepository,
		ISecretaryRepository secretaryRepository,
		UserManager<ApplicationUser> userManager,
		IMapper mapper)
	{
		_messageRepository = messageRepository;
		_studentRepository = studentRepository;
		_secretaryRepository = secretaryRepository;
		_userManager = userManager;
		_mapper = mapper;
	}

	/// <summary>
	/// Displays the message index page with optional receiver type, content, and course name.
	/// </summary>
	/// <param name="RecieverType">The type of the message receiver (student or secretary).</param>
	/// <param name="Contentvalue">The content of the message (optional).</param>
	/// <param name="coursename">The name of the course (optional).</param>
	/// <returns>The Index view.</returns>
	public IActionResult Index(string RecieverType, string? Contentvalue, string? coursename)
	{
		ViewBag.RecieverType = RecieverType;
		ViewBag.Contentvalue = Contentvalue;
		ViewBag.coursename = coursename;
		return View();
	}

	/// <summary>
	/// Adds a new message to the database and redirects to the appropriate page.
	/// </summary>
	/// <param name="message">The message to add.</param>
	/// <param name="RecieverType">The type of the message receiver.</param>
	/// <param name="Contentvalue">The content of the message (optional).</param>
	/// <param name="coursename">The name of the course (optional).</param>
	/// <returns>Redirects to the Home or Secretary index page, or NotFound if the receiver is invalid.</returns>
	public async Task<IActionResult> AddMessage(Message message, string? RecieverType, string? Contentvalue, string? coursename)
	{
		var currentUser = await _userManager.GetUserAsync(User);

		if (RecieverType == "Secertary" && RecieverType is not null)
		{
			return await HandleStudentToSecretaryMessage(currentUser, message, coursename);
		}
		else
		{
			return await HandleSecretaryToStudentMessage(currentUser, message, RecieverType, Contentvalue, coursename);
		}
	}

	/// <summary>
	/// Shows messages for the current user, filtered by kind (student or secretary).
	/// </summary>
	/// <param name="kind">The kind of user ("student" or "secertary").</param>
	/// <returns>The ShowMessages view with the relevant messages.</returns>
	public async Task<IActionResult> ShowMessages(string? kind)
	{
		var currentUser = await _userManager.GetUserAsync(User);
		var messagelist = new Dictionary<string, List<string>>();

		if (kind?.ToLower() == "student")
		{
			var messages = await _messageRepository.GetMessagesForStudentAsync(currentUser.Id);
			ProcessMessages(messages, messagelist);
		}
		else if (kind?.ToLower() == "secertary")
		{
			var messages = await _messageRepository.GetMessagesForSecretaryAsync(currentUser.Id);
			ProcessMessages(messages, messagelist);

			var stdSender = messages.Select(m => m.StudentSender).FirstOrDefault();
			ViewBag.StdSender = stdSender;
		}

		ViewBag.allmessages = messagelist;
		return View();
	}

	/// <summary>
	/// Processes a list of messages and organizes them by sender name.
	/// </summary>
	/// <param name="messages">The list of messages to process.</param>
	/// <param name="messagelist">The dictionary to populate with sender names and their messages.</param>
	private void ProcessMessages(List<Message> messages, Dictionary<string, List<string>> messagelist)
	{
		foreach (var m in messages)
		{
			if ((m.StudentReciver != null && m.SecertarySender != null) ||
				(m.SecertaryReciver != null && m.StudentSender != null))
			{
				if (messagelist.ContainsKey(m.SenderName))
				{
					messagelist[m.SenderName].Add(m.Content);
				}
				else
				{
					messagelist[m.SenderName] = new List<string> { m.Content };
				}
			}
		}
	}

	/// <summary>
	/// Handles sending a message from a student to a secretary.
	/// </summary>
	/// <param name="currentUser">The current application user.</param>
	/// <param name="message">The message to send.</param>
	/// <param name="coursename">The name of the course (optional).</param>
	/// <returns>Redirects to the Home index page.</returns>
	private async Task<IActionResult> HandleStudentToSecretaryMessage(ApplicationUser currentUser, Message message, string? coursename)
	{
		var student = await _studentRepository.GetStudentBySecurityKeyAsync(currentUser.Id);
		if (student == null)
		{
			student = _mapper.Map<Student>(currentUser);
			await _studentRepository.AddStudentAsync(student);
		}

		var secretary = await _secretaryRepository.GetFirstSecretaryAsync();

		await _messageRepository.AddMessageAsync(new Message
		{
			StudentSender = student,
			Content = $"{message.Content} \n [course : {coursename}]",
			SecertaryReciver = secretary,
			Date = DateTime.Now,
			SenderName = student.FullName,
			IsReplyed = false,
		});

		return RedirectToAction("Index", "Home");
	}

	/// <summary>
	/// Handles sending a message from a secretary to a student or another secretary.
	/// </summary>
	/// <param name="currentUser">The current application user.</param>
	/// <param name="message">The message to send.</param>
	/// <param name="RecieverType">The receiver's security key.</param>
	/// <param name="Contentvalue">The content of the message (optional).</param>
	/// <param name="coursename">The name of the course (optional).</param>
	/// <returns>Redirects to the appropriate index page or NotFound if the receiver is invalid.</returns>
	private async Task<IActionResult> HandleSecretaryToStudentMessage(
		ApplicationUser currentUser,
		Message message,
		string? RecieverType,
		string? Contentvalue,
		string? coursename)
	{
		var studentReciever = await _studentRepository.GetStudentBySecurityKeyAsync(RecieverType);
		var secretaryReciever = await _secretaryRepository.GetSecretaryBySecurityKeyAsync(RecieverType);

		if (studentReciever != null)
		{
			var secretarySender = await _secretaryRepository.GetSecretaryBySecurityKeyAsync(currentUser.Id);

			await _messageRepository.AddMessageAsync(new Message
			{
				SecertarySender = secretarySender,
				Content = $"{message.Content} \n [course : {coursename}]",
				StudentReciver = studentReciever,
				Date = DateTime.Now,
				SenderName = secretarySender.FullName,
			});

			await MarkMessagesAsReplied(secretarySender, Contentvalue);

			return RedirectToAction("Index", "Secrtary");
		}
		else if (secretaryReciever != null)
		{
			var studentSender = await _studentRepository.GetStudentBySecurityKeyAsync(currentUser.Id);

			await _messageRepository.AddMessageAsync(new Message
			{
				StudentSender = studentSender,
				Content = $"{message.Content} \n [course : {coursename}]",
				SecertaryReciver = secretaryReciever,
				Date = DateTime.Now,
				SenderName = studentSender.FullName,
			});

			await MarkMessagesAsReplied(studentSender, Contentvalue);

			return RedirectToAction("Index", "Home");
		}

		return NotFound();
	}

	/// <summary>
	/// Marks messages as replied for a given student.
	/// </summary>
	/// <param name="student">The student whose messages should be marked as replied.</param>
	/// <param name="contentValue">The content value to match.</param>
	private async Task MarkMessagesAsReplied(Student student, string contentValue)
	{
		var messages = await _messageRepository.GetMessagesForStudentAsync(student.SecurtyKey);
		foreach (var item in messages)
		{
			if (item.Content == contentValue)
			{
				item.IsReplyed = true;
				await _messageRepository.UpdateMessageAsync(item);
			}
		}
	}

	/// <summary>
	/// Marks messages as replied for a given secretary.
	/// </summary>
	/// <param name="secretary">The secretary whose messages should be marked as replied.</param>
	/// <param name="contentValue">The content value to match.</param>
	private async Task MarkMessagesAsReplied(Secertary secretary, string contentValue)
	{
		var messages = await _messageRepository.GetMessagesForSecretaryAsync(secretary.SecurtyKey);
		foreach (var item in messages)
		{
			if (item.Content == contentValue)
			{
				item.IsReplyed = true;
				await _messageRepository.UpdateMessageAsync(item);
			}
		}
	}
}
