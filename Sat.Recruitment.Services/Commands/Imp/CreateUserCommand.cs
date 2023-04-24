using Sat.Recruitment.DTOs.Models;
using Sat.Recruitment.DTOs.Requests;
using Sat.Recruitment.DTOs.Responses;
using Sat.Recruitment.EF.Context;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Commands.Imp
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly ApplicationDbContext context;

        public CreateUserCommand(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<UserCreateResponse> Execute(UserCreateRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                Phone = request.Phone,
                UserType = request.UserType,
                Money = request.Money
            };
            context.Add(user);
            await context.SaveChangesAsync();

            var response = new UserCreateResponse
            {
                Success = true,
                User = user
            };

            return response;
        }
    }
}