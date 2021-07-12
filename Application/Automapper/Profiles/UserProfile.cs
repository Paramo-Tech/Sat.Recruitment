using Application.Automapper.Resolvers;
using Application.Commands;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;


namespace Application.Automapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommand, User>().ForMember(
            dest => dest.Money,
            opt => opt.MapFrom<UserMoneyResolver>());

            CreateMap<User, UserDto>();
        }
    }
}
