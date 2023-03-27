using AutoMapper;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Models;
using System.Diagnostics.CodeAnalysis;

namespace Sat.Recruitment.Api.MapperProfile
{
    [ExcludeFromCodeCoverage]
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, ReadUserDTO>();
            CreateMap<ReadUserDTO, User>();

            CreateMap<CreateUserDTO, ReadUserDTO>();
            CreateMap<ReadUserDTO, CreateUserDTO>();

            CreateMap<User, CreateUserDTO>();
            CreateMap<CreateUserDTO, CreateUserDTO>();
        }
    }
}
