using AutoMapper;
using Sat.Recruitment.Application.Dto.User;
using Sat.Recruitment.Domain.Model;

namespace Sat.Recruitment.Application.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Address, act => act.MapFrom(x => x.Address))
                .ForMember(dest => dest.Email, act => act.MapFrom(x => x.Email))
                .ForMember(dest => dest.Name, act => act.MapFrom(x => x.Name))
                .ForMember(dest => dest.Phone, act => act.MapFrom(x => x.Phone))
                .ForMember(dest => dest.UserType, act => act.MapFrom(x => x.UserType));
        }
    }
}
