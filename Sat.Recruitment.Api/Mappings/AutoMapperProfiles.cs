using AutoMapper;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;

namespace Sat.Recruitment.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType))
                .ReverseMap();
        }
    }
}
