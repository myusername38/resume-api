using System;
using AutoMapper;
using ResumeApi.Dtos.Solution;
using ResumeApi.Dtos.User;
using ResumeApi.Models;

namespace ResumeApi.Utils
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<User, CreateUserDto>().ReverseMap();

        }
	}
}

