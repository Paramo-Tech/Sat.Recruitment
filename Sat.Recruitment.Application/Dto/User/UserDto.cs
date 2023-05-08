using Sat.Recruitment.Domain.Enum;

namespace Sat.Recruitment.Application.Dto.User
{
    /// <summary>
    /// Data transfer that contains information from a User.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the full name of the User.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the User.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the physical address of the User.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the User.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the user type.
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Gets or sets the money of the User.
        /// </summary>
        public decimal Money { get; set; }
    }
}
