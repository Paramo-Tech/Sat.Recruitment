using AutoMapper;
using Sat.Recruitment.Api.Business.DTOs;
using Sat.Recruitment.Api.Business.Entities;

namespace Sat.Recruitment.Api.Data.Mappings
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
