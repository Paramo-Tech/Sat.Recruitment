using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sat.Recruitment.Application.Common.Mappings;
using Sat.Recruitment.Application.Common.Validators;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Application.Users.Models
{
    /// <summary>
    /// Contains the User's information
    /// </summary>
    public class UserViewModel : IMapFrom<User>
    {
        /// <summary>
        /// User's full name 
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Valid email address
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User's address lines
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Valid phone number, including country code
        /// </summary>
        [Required]
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// UserTypes: Normal, SuperUser or Premium
        /// </summary>
        [Required]
        [UserTypeValidator]
        public UserTypes UserType { get; set; }

        /// <summary>
        /// Numeric value that represents the User's money
        /// </summary>
        [Required]
        [Column(TypeName = "DECIMAL(18,4)")]
        public decimal Money { get; set; }
    }
}
