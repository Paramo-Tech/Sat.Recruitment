using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sat.Recruitment.Token.Models.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Token.Services
{
    public class AccountService
    {
        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Authenticate(AccountDto accountDto)
        {
            var token = string.Empty;

            var user = _configuration.GetSection("ApiWebCredentials:Username").Value;
            var pass = _configuration.GetSection("ApiWebCredentials:Password").Value;

            if (user == accountDto.Username && pass == accountDto.Password)
            {
                token = JwtToken();
            }

            return token;
        }

        private string JwtToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Constants.ClaimsInformation.Name),
                    new Claim(ClaimTypes.Email, Constants.ClaimsInformation.Email),
                    new Claim(ClaimTypes.GivenName, Constants.ClaimsInformation.GivenName),
                    new Claim(ClaimTypes.Role, Constants.ClaimsInformation.Role),
                    new Claim(ClaimTypes.Country, Constants.ClaimsInformation.Country)

                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var authToken = tokenHandler.WriteToken(token);

            return authToken;
        }
    }
}
