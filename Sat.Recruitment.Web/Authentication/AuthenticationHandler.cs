using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sat.Recruitment.Domain.Interfaces;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Sat.Recruitment.Web.Authentication
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public AuthenticationHandler(IUserService userService,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string userType;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                var user = credentials.FirstOrDefault();
                var password = credentials.LastOrDefault();

                var authenticationIsValid = await _userService.ValidateCredentials(user, password);

                if (!authenticationIsValid)
                {
                    throw new ArgumentException("Invalid credentials");
                }

                userType = await _userService.GetUserTypeByEmail(user);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
            }

            var claims = new[] { new Claim(ClaimTypes.Role, userType) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

    }
}
