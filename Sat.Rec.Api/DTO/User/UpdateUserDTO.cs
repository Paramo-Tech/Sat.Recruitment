﻿namespace Sat.Rec.Api.DTO
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int UserTypeId { get; set; }
        public decimal Money { get; set; }
    }
}
