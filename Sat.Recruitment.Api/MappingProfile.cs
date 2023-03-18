using AutoMapper;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using System;
using System.Globalization;

namespace Sat.Recruitment.Api
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<UserRequest, User>()
                .ForMember(dest => dest.Money,
                            opt => opt.MapFrom(src => Convert.ToDouble(src.Money, CultureInfo.InvariantCulture)))
                .ReverseMap()
                .ForMember(dest => dest.Money,
                            opt => opt.MapFrom(src => src.Money.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
