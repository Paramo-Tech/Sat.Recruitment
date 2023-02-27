using Microsoft.AspNetCore.Builder;
using Sat.Recruitment.Api.Repository.Interfaces;

namespace Sat.Recruitment.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedUsers(this IApplicationBuilder app, IUserSeeder userSeeder)
        {
            userSeeder.SeedUsers();
            return app;
        }
    }
}
