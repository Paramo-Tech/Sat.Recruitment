using MediatR;

namespace Sat.Recruitment.Api.Services.Commands
{
    public class CreateUserCommand: IRequest<int>
    {
        public string name {get; set;}
        
        public string email { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public string userType { get; set; }

        public string money { get; set; }
    }
}
