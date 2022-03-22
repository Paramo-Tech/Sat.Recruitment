using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Forms
{
    public class UserEditionForm 
    {
        public ulong Id { get; set; }
        public string Address { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
    }
}
