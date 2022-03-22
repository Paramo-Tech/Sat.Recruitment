using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Repository.Users;
using Sat.Recruitment.Services.Users.Commands;
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

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                fv.RegisterValidatorsFromAssemblyContaining<TestsStartUp>();
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JWT_POLICY, policy =>
                {
                    policy.RequireAuthenticatedUser().AddAuthenticationSchemes(TestAuthenticationOptions.SCHEME);
                });
            });

            services.AddAuthentication().AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(TestAuthenticationOptions.SCHEME, null);
                        
            services.AddAutoMapper(typeof(TestsStartUp));
            services.AddDbContext<UsersContext>(opt => TestHelper.GenerateInMemoryContext());
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddMediatR(typeof(CreateUserHandler).Assembly);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
