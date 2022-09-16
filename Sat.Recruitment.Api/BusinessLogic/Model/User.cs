using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.BusinessLogic.Model
{
    public class User
    {
        public static string NORMAL = "normal";
        public static string SUPERUSER = "superuser";
        public static string PREMIUM = "premium";

        [Required(ErrorMessage = "The field {0} is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        [Phone]
        public string Phone { get; set; }

        private string _userType = User.NORMAL;
        public string UserType {
            get { return _userType; }
            set { this._userType = value?.Trim().ToLower(); } 
        }

        public decimal Money { get; set; } = 0;
    }
}
