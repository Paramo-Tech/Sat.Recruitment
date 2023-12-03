namespace Sat.Recruitment.Api.Domain
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        [Required(ErrorMessage = "Name es required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email es required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address es required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone es required.")]
        [Phone(ErrorMessage = "Invalid Phone format.")]
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}
