using AutoMapper;
using CenterDragon.Models.Entites;
using CenterDragon.View_Models;

namespace CenterDragon.Profiles
{
	public class ParticipantProfile : Profile
	{
		public ParticipantProfile()
		{
			// Participant to ViewModel
			CreateMap<Participants, participantViewModel>()
				.ForMember(dest => dest.ParCourseId, opt => opt.MapFrom(src => src.CourseId))
				.ForMember(dest => dest.Courselist, opt => opt.Ignore());

			// ViewModel to Participant
			CreateMap<participantViewModel, Participants>()
				.ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.ParCourseId));
		}
	}
}
