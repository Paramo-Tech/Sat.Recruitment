using AutoMapper;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Models;

namespace Sat.Recruitment.Api.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => 
                dest.Money,
                opt => opt.MapFrom(src => src.Money.ToString()))
                .ReverseMap();
        }
    }
}
