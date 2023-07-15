using System.Security.Policy;
using System;

namespace Sat.Recruitment.Api.Application.Request
{
    public class UserDTO
    {
        public UserDTO()
        {
            
        }
        public UserDTO(string name ,string email,string address,string phone ,string userType,string money)
        {
            this.Name = name;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.UserType = UserType;
            if (UserType == "Normal")
            {
                if (decimal.Parse(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = decimal.Parse(money) * percentage;
                    this.Money = this.Money + gif;
                }
                if (decimal.Parse(money) < 100)
                {
                    if (decimal.Parse(money) > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = decimal.Parse(money) * percentage;
                        this.Money = this.Money + gif;
                    }
                }
            }
            if (this.UserType == "SuperUser")
            {
                if (decimal.Parse(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = decimal.Parse(money) * percentage;
                    this.Money = this.Money + gif;
                }
            }
            if (this.UserType == "Premium")
            {
                if (decimal.Parse(money) > 100)
                {
                    var gif = decimal.Parse(money) * 2;
                    this.Money = this.Money + gif;
                }
            }
        }

        public string Address  {get;set;}
        public string Phone    {get;set;}
        public string Name     {get;set;}
        public string Email    {get;set;}
        public string UserType {get;set;}
        public decimal Money   {get;set;}
                               
    }

}
