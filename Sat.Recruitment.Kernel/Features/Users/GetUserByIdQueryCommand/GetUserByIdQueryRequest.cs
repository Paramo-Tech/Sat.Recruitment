using MediatR;
using Sat.Recruitment.Core.Models.User;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Kernel.Features.Users.GetUserQueryByIdCommand
{
    /// <summary>
    /// Request to get user by id
    /// </summary>
    public class GetUserByIdQueryRequest : IRequest<IUser>
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        public string? Id { get; set; }
    }
}
