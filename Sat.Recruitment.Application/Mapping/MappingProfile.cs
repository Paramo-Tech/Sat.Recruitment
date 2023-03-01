using Sat.Recruitment.Application.DTOs;
using Sat.Recruitment.Domain.Entities;
using AutoMapper;

namespace Sat.Recruitment.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
