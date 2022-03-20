using MediatR;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Users.Commands
{
    public class EditUserCommand : IRequest<User>
    {
        public UserEditionForm User { get; set; }
        public EditUserCommand(UserEditionForm user) => User = user;
    }
}
