using Sat.Recruitment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Entities
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        private decimal _money;
        public decimal Money 
                {
                  get { return _money; }
                  set { _money =  (value > 100 && UserType != null) ? (value + (value * UserType.Percentage)) : value; }
                }
        public UserType UserType { get; set; }

    }
}
