using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sat.Recruitment.Api.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sat.Recruitment.Api.Utilities
{
    public class Util : IUtil
    {
        private readonly IConfiguration _configuration;

        public Util(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public decimal? MoneyTypeNormal(decimal? money)
        {
            ValidateDecimal(ref money);
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.12);

            if (money < 100 && money > 10)
                percentage = Convert.ToDecimal(0.8);

            return money + (money * percentage);
        }

        public decimal? MoneyTypePremium(decimal? money)
        {
            ValidateDecimal(ref money);
            decimal? gif = 0;
            if (money > 100)
                gif = money * 2;
            
            return money + gif;
        }

        public decimal? MoneyTypeSuperUser(decimal? money)
        {
            ValidateDecimal(ref money);
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.20);

            return money + (money * percentage);
        }

        public string NormaliceEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        private void ValidateDecimal(ref decimal? money)
        {
            if (money == null)
                money = 0;
        }

        public string Md5_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (MD5 hash = MD5.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public string TokenJWT(string email)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            
            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] { new Claim("emaiil", email), new Claim(JwtRegisteredClaimNames.Email, email)};

            var _Payload = new JwtPayload(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                     expires: DateTime.UtcNow.AddMinutes(15)
                );

            var _Token = new JwtSecurityToken(_Header,_Payload);

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
