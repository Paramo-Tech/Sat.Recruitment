using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models
{
    public partial class Type
    {
        public Type()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
