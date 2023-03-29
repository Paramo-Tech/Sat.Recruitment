using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models
{
    public class UserRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string UserType { get; set; }

        public decimal Money { get; set; }
    }
}
