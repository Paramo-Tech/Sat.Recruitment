using AutoMapper;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTOs;

namespace Sat.Recruitment.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UserModelDto, UserModel>().ReverseMap();
                config.CreateMap<UserTypeModelDto, UserTypeModel>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
