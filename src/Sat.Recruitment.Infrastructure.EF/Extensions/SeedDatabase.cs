using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Enums;
using Sat.Recruitment.Infrastructure.EF.EFSpecifics;
using System;
using System.Linq;

namespace Sat.Recruitment.Infrastructure.EF.Extensions
{
    public static class SeedDatabaseExtension
    {
        public static IServiceCollection SeedDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            RecruitmentDbContext dbContext = serviceProvider.GetService<RecruitmentDbContext>();

            User user1 = dbContext.Users.FirstOrDefault(u => u.Id == Guid.Parse("32fbf1e2-2d45-481d-842b-f9a691cdcbdc"));
            if (user1 == null)
            {
                user1 = new User () { Id = Guid.Parse("32fbf1e2-2d45-481d-842b-f9a691cdcbdc"), Name = "Juan", Email = "Juan@marmol.com", Address = "Peru 2464", Phone = "+5491154762312", UserType = UserType.Normal, Money = 1234 };
                dbContext.Users.Add(user1);
            }

            User user2 = dbContext.Users.FirstOrDefault(u => u.Id == Guid.Parse("b3e7f5ab-c1a0-4795-ba41-87ec9a2184a5"));
            if (user2 == null)
            {
                user2 = new User() { Id = Guid.Parse("b3e7f5ab-c1a0-4795-ba41-87ec9a2184a5"), Name = "Franco", Email = "Franco.Perez@gmail.com", Address = "Alvear y Colombres", Phone = "+534645213542", UserType = UserType.SuperUser, Money = 112234 };
                dbContext.Users.Add(user2);
            }

            User user3 = dbContext.Users.FirstOrDefault(u => u.Id == Guid.Parse("cf3fae61-ca59-46a1-9fc0-b33c2e65acb0"));
            if (user3 == null)
            {
                user3 = new User() { Id = Guid.Parse("cf3fae61-ca59-46a1-9fc0-b33c2e65acb0"), Name = "Agustina", Email = "Agustina@gmail.com", Address = "Garay y Otra Calle", Phone = "+534645213542", UserType = UserType.Premium, Money = 112234 };
                dbContext.Users.Add(user3);
            }

            User user4 = dbContext.Users.FirstOrDefault(u => u.Id == Guid.Parse("91ab1042-03d1-4490-be57-3e94cd319960"));
            if (user4 == null)
            {
                user4 = new User() { Id = Guid.Parse("91ab1042-03d1-4490-be57-3e94cd319960"), Name = "Emanuel", Email = "Emanuel@gmail.com", Address = "Otra Calle y Otra Calle", Phone = "+5491154762312", UserType = null, Money = 700 };
                dbContext.Users.Add(user4);
            }

            dbContext.SaveChanges();

            return services;
        }
    }
}
