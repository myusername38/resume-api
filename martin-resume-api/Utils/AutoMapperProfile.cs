using System;
using AutoMapper;
using martin_resume_api.Dtos;
using martin_resume_api.Entities;

namespace martin_resume_api.Utils
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<User, UserDto>().ReverseMap();
		}
	}
}

