using Application.Mappers;
using Domain.Entities;

namespace Application.Models
{
    public class UserCreationDto : IMapFrom<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string Money { get; set; }
    }
}