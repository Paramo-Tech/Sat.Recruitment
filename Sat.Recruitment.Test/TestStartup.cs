using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Test
{
    public class TestsStartUp : Startup
    {
        private const string JWT_POLICY = "JwtPolicy";

        public TestsStartUp(IConfiguration configuration) : base(configuration)
        {

        }

        protected void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JWT_POLICY, policy =>
                {
                    policy.RequireAuthenticatedUser().AddAuthenticationSchemes(TestAuthenticationOptions.SCHEME);
                });
            });

            services.AddAuthentication().AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(TestAuthenticationOptions.SCHEME, null);
        }
    }
}
