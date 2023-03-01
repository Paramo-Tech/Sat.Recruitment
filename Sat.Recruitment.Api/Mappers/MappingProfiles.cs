using AutoMapper;
using Sat.Recruitment.Api.Dtos;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Commands;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Mappers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserTypeString.GetByName(src.UserType))); ;
            CreateMap<User, CreateUserCommand>()
                .ForMember(dest => dest.userType, opt => opt.MapFrom(src => src.UserType.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserTypeString.GetByName(src.userType)));
            
        }
    }
}
