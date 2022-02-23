using Sat.Recruitment.Core.Enums;
using System;

namespace Sat.Recruitment.Api.DTOs.Users
{
    public class ListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType? UserType { get; set; }
        public decimal Money { get; set; }
    }
}
