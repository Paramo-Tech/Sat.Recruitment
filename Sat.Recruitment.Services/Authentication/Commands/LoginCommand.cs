using MediatR;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Authentication.Commands
{
    public class LoginCommand : IRequest<AuthenticatedUser>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public LoginCommand(string email, string password, string key)
        {
            Email = email;
            Password = password;
            Key = key;  
        }
    }
}
