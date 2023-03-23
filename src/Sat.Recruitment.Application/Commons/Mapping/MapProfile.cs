using System;
using AutoMapper;
using Sat.Recruitment.Application.Commands.CreateUser;
using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.Application.Commons.Mapping
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<CreateUserCommand, User>();
			CreateMap<User, CreateUserCommandResponse>();
		}
	}
}

