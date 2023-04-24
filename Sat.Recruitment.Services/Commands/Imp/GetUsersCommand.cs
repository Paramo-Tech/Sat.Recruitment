using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.DTOs.Models;
using Sat.Recruitment.EF.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Commands.Imp
{
    public class GetUsersCommand : IGetUsersCommand
    {
        private readonly ApplicationDbContext context;

        public GetUsersCommand(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<User>> Execute()
        {
            return await context.Users.ToListAsync();
        }
    }
}