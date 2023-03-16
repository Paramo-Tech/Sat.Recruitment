using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultDataAsync(ApplicationDbContext context)
        {

            var x = context.UserTypes.ToList();
            if (!context.UserTypes.Any())
            {
                context.UserTypes.AddRange(
                            new UserType { Name = "Normal", Percentage = Convert.ToDecimal(0.12) },
                            new UserType { Name = "SuperUser", Percentage = Convert.ToDecimal(0.20) },
                            new UserType { Name = "Premium", Percentage = Convert.ToDecimal(2) }
                            );

                await context.SaveChangesAsync();

            }

            if (!context.Users.Any())
            {
                
                var reader = ReadUsersFromFile();
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    string type = line.Split(',')[4].ToString();
                    decimal m = decimal.Parse(line.Split(',')[5].ToString());
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = context.UserTypes.FirstOrDefault(x => x.Name == type),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    context.Users.Add(user);
                }
                await context.SaveChangesAsync();
                reader.Close();
            }

            //     var administratorRole = new IdentityRole("Administrator");

            // if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            // {
            //     await roleManager.CreateAsync(administratorRole);
            // }

            // var defaultUser = new ApplicationUser { UserName = "iayti", Email = "test@test.com" };

            // if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            // {
            //     await userManager.CreateAsync(defaultUser, "Matech_1850");
            //     await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
            // }
        }

        private static StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
