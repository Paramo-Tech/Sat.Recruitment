﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}
