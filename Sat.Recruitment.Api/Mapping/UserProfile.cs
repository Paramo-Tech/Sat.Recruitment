using AutoMapper;
using Sat.Recruitment.Application.Dto;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Api.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
