using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? UserType { get; set; }
        public decimal? Money { get; set; }

        public virtual Type UserTypeNavigation { get; set; }
    }
}
