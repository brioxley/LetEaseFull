using AutoMapper;
using LetEase.Application.DTOs;
using LetEase.Domain.Entities;

namespace LetEase.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDto>().ReverseMap();
			CreateMap<CreateUserDto, User>();
			CreateMap<UpdateUserDto, User>();
			CreateMap<Company, CompanyDto>().ReverseMap();
			CreateMap<CreateCompanyDto, Company>();
			CreateMap<UpdateCompanyDto, Company>();
			CreateMap<Property, PropertyDto>().ReverseMap();
			CreateMap<CreatePropertyDto, Property>();
			CreateMap<UpdatePropertyDto, Property>();
		}
	}
}