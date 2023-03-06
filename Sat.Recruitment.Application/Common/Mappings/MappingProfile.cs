using AutoMapper;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>();
        }
    }
}