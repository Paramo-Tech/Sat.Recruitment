using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Models
{
    public class AuthenticatedUser
    {
        public string UserName { get; set; }
        public string UserRol { get; set; }
        public ulong UserId { get; set; }
        public string token { get; set; }
    }
}
