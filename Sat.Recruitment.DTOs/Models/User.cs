using Sat.Recruitment.DTOs.Enums;
using System;

namespace Sat.Recruitment.DTOs.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public double Money { get; set; }
    }
}