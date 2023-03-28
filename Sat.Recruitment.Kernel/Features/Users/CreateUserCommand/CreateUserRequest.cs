using MediatR;

namespace Sat.Recruitment.Kernel.Features.Users.CreateUserCommand
{
    public class CreateUserRequest : IRequest<object>
    {
        /// <summary>
        /// User's address. Maximum Length 200 characters.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// User's money.
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// Users' phone number. It should be unique.  Maximum Length 50 characters.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// User's type. Could be: Normal, SuperUser or Premium.
        /// </summary>
        public string? UserType { get; set; }

        /// <summary>
        /// User's email. It should be unique. Maximum Length 100 characters.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// User's name. It should be unique. Maximum Length 200 characters.
        /// </summary>
        public string? Name { get; set; }
    }
}
