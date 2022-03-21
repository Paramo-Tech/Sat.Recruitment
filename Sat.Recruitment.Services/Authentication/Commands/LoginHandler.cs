using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Authentication.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthenticatedUser>
    {
        private readonly UsersContext _context;
        public LoginHandler(UsersContext context) => _context = context;

        public async Task<AuthenticatedUser> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var cryptedKey = Encoding.ASCII.GetBytes(request.Key);
            var cryptedPassword = Encoding.ASCII.GetBytes(request.Password);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == cryptedPassword);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, nameof(user.UserType)),
                    new Claim(ClaimTypes.GivenName, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(cryptedKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var writtenToken = tokenHandler.WriteToken(token);

            return new AuthenticatedUser
            {
                UserId = user.Id,
                UserName = user.Email,
                UserRol = nameof(user.UserType),
                token = writtenToken
            };
        }

        public Task Handle(LoginCommand request, object none)
        {
            throw new NotImplementedException();
        }
    }
}
