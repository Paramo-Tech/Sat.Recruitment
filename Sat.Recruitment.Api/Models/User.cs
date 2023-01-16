using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public string phone { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public string userType { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public decimal money { get; set; }
        public override string ToString()
        {
            return name + "," + email + "," + address + "," + phone + "," + userType + "," + money.ToString();
        }
    }
}
