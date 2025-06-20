using AutoMapper;
using CenterDragon.Data;
using CenterDragon.Models.Entites;

namespace CenterDragon.Profiles
{
	public class StudentProfile : Profile
	{
		public StudentProfile()
		{
			CreateMap<ApplicationUser, Student>()
				.ForMember(dest => dest.SecurtyKey, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.password, opt => opt.MapFrom(src => src.PasswordHash))
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
				.ForMember(dest => dest.Ediation, opt => opt.MapFrom(src => src.Ediation))
				.ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress));
		}
	}
}
