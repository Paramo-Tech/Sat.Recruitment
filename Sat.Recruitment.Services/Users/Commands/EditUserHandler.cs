using MediatR;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Users.Commands
{
    public class EditUserHandler : IRequestHandler<EditUserCommand, User>
    {
        private readonly UsersContext _context;
        public EditUserHandler(UsersContext context) => _context = context;
        public async Task<User> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == (ulong)request.User.Id);

            if (user == null)
                return null;

            byte[] cryptedPassword = null;
            if (!String.IsNullOrEmpty(request.User.Password))
                cryptedPassword = Encoding.ASCII.GetBytes(request.User.Password);

            if (!String.IsNullOrEmpty(request.User.Phone) && request.User.Phone != user.Phone)
                user.Phone = request.User.Phone;
            if (!String.IsNullOrEmpty(request.User.Email) && request.User.Email != user.Email)
                user.Email = request.User.Email;
            if (!String.IsNullOrEmpty(request.User.Address) && request.User.Address != user.Address)
                user.Address = request.User.Address;
            if ((UserTypeEnum)request.User.UserType != user.UserType)
                user.UserType = (UserTypeEnum)request.User.UserType;
            if (cryptedPassword != null && cryptedPassword != user.Password)
                user.Password = cryptedPassword;

            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}
