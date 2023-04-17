using System;
namespace Sat.Recruitment.Api.Entities.Dto
{
    public class UserDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string userType { get; set; }
        public decimal money { get; set; }
    }
}

