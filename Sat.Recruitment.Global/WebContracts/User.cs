using System;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Global.WebContracts
{
    public class User
    {
        public User() { }

        public User(string name, string email, string address, string phone, string userType, string money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = decimal.Parse(money);
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public string UserType { get; set; }

        public decimal Money { get; set; }

    }
}

