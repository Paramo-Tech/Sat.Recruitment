using MediatR;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Requests.Commands
{
    public class AddUserCommand : IRequest<User>
    {
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string userType { get; set; }
        public string money { get; set; }
    }
}
