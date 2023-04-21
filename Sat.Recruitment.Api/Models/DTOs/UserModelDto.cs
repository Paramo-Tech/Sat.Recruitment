using System;

namespace Sat.Recruitment.Api.Models.DTOs
{
    public class UserModelDto
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public Guid UserTypeId { get; set; }

        public decimal? Money { get; set; }
    }
}
