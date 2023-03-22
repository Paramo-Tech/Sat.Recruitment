using System;
using System.Net;
using System.Numerics;
using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.Infrastructure.Data.Seed
{
	public class UserSeedContext
	{
		public static void SeedAsync(SatDbContext dbContext)
		{
			if (!dbContext.Users.Any())
			{
				var users = new List<User>
				{
					new User("Juan", "Juan@marmol.com", "+5491154762312", "Peru 2464", "Normal", 1234),
					new User("Franco", "Franco.Perez@gmail.com", "+534645213542", "Alvear y Colombres", "Premium", 112234),
					new User("Agustina", "Agustina@gmail.com", "+534641213542", "Garay y Otra Calle", "SuperUser", 112234)
				};

				dbContext.Users.AddRange(users);
				dbContext.SaveChanges();
			}
		}
	}
}

