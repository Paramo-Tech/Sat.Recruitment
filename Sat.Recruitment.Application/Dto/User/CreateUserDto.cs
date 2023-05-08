using Sat.Recruitment.Domain.Enum;

namespace Sat.Recruitment.Application.Dto.User
{
    /// <summary>
    /// Data transfer object used to create Users.
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// Gets or sets the name of the new User.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the new User.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the physical adress of the new User.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone numer of the new User.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the user type.
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Gets or sets the money of the new User.
        /// </summary>
        public decimal Money { get; set; }
    }
}
