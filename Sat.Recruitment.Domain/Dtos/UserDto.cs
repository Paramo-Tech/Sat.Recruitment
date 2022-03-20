using Sat.Recruitment.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Domain.Forms;

namespace Sat.Recruitment.Domain.Dtos
{
    public class UserDto 
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Money { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int UserType { get; set; }
        public string UserTypeString
        {
            get
            {
                switch (UserType)
                {
                    case 1:
                        return nameof(UserTypeEnum.NORMAL);
                        break;
                    case 2:
                        return nameof(UserTypeEnum.SUPERUSER);
                        break;
                    case 3:
                        return nameof(UserTypeEnum.PREMIUM);
                        break;
                    default:
                        return string.Empty;
                        break;
                }
            }
        }
    }
}
