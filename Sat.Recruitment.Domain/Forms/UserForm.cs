using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Forms
{
    public abstract class UserForm 
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
    }
}
