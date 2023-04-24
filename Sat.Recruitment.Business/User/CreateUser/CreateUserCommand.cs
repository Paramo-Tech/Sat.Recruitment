using MediatR;

namespace Sat.Recruitment.Business.User.CreateUser
{ 
    public class CreateUserCommand : IRequest<CreateUserResult>
    {
        public string name { get; set; }

        public string email { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public string userType { get; set; }

        public string money { get; set; }
    }

}
