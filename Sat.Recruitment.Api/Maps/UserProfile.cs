using AutoMapper;
using Sat.Recruitment.Api.ViewModel;
using Sat.Recruitment.Domain.Model;

namespace Sat.Recruitment.Api.Maps
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, User>();
        }
    }
}
