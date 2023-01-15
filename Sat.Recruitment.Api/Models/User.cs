using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string userType { get; set; }
        [Required]
        public decimal money { get; set; }
        public override string ToString()
        {
            return name + "," + email + "," + address + "," + phone + "," + userType + "," + money.ToString();
        }
    }
}
