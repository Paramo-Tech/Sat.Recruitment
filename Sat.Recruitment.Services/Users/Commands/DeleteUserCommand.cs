using MediatR;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Users.Commands
{
    public class DeleteUserCommand : IRequest<User>
    {
        public ulong Id { get; set; }
        public DeleteUserCommand(ulong id) => Id = id;
    }
}
