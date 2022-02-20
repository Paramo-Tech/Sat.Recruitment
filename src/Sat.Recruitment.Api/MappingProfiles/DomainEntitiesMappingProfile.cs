using AutoMapper;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Core.DomainEntities;

namespace Sat.Recruitment.Api.MappingProfiles
{
    public class DomainEntitiesMappingProfile : Profile
    {
        public DomainEntitiesMappingProfile()
        {
            CreateMap<CreateUserRequest, User>()
            .ForMember(domain => domain.Name, m => m.MapFrom(dto => dto.Name))
            .ForMember(domain => domain.Email, m => m.MapFrom(dto => dto.Email))
            .ForMember(domain => domain.Address, m => m.MapFrom(dto => dto.Address))
            .ForMember(domain => domain.Phone, m => m.MapFrom(dto => dto.Phone))
            .ForMember(domain => domain.UserType, m => m.MapFrom(dto => dto.UserType))
            .ForMember(domain => domain.Money, m => m.MapFrom(dto => dto.Money));

            CreateMap<User, CreateUserResponse>()
            .ForMember(dto => dto.Id, m => m.MapFrom(domain => domain.Id))
            .ForMember(dto => dto.Name, m => m.MapFrom(domain => domain.Name))
            .ForMember(dto => dto.Email, m => m.MapFrom(domain => domain.Email))
            .ForMember(dto => dto.Address, m => m.MapFrom(domain => domain.Address))
            .ForMember(dto => dto.Phone, m => m.MapFrom(domain => domain.Phone))
            .ForMember(dto => dto.UserType, m => m.MapFrom(domain => domain.UserType))
            .ForMember(dto => dto.Money, m => m.MapFrom(domain => domain.Money));

            CreateMap<User, ListUsersResponse>()
            .ForMember(dto => dto.Id, m => m.MapFrom(domain => domain.Id))
            .ForMember(dto => dto.Name, m => m.MapFrom(domain => domain.Name))
            .ForMember(dto => dto.Email, m => m.MapFrom(domain => domain.Email))
            .ForMember(dto => dto.Address, m => m.MapFrom(domain => domain.Address))
            .ForMember(dto => dto.Phone, m => m.MapFrom(domain => domain.Phone))
            .ForMember(dto => dto.UserType, m => m.MapFrom(domain => domain.UserType))
            .ForMember(dto => dto.Money, m => m.MapFrom(domain => domain.Money));

            CreateMap<User, GetByIdResponse>()
            .ForMember(dto => dto.Id, m => m.MapFrom(domain => domain.Id))
            .ForMember(dto => dto.Name, m => m.MapFrom(domain => domain.Name))
            .ForMember(dto => dto.Email, m => m.MapFrom(domain => domain.Email))
            .ForMember(dto => dto.Address, m => m.MapFrom(domain => domain.Address))
            .ForMember(dto => dto.Phone, m => m.MapFrom(domain => domain.Phone))
            .ForMember(dto => dto.UserType, m => m.MapFrom(domain => domain.UserType))
            .ForMember(dto => dto.Money, m => m.MapFrom(domain => domain.Money));
        }
    }
}
