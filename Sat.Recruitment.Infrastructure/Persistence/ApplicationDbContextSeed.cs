using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    private readonly ApplicationDbContext _applicationDbContext;
    public ApplicationDbContextSeed(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_applicationDbContext.Database.IsSqlServer())
            {
                await _applicationDbContext.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            //TODO: log error
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            if (!(await _applicationDbContext.Users.AnyAsync()))
            {
                var users = new List<User>()
                {
                    new User
                    {
                        Name = "Juan",
                        Email = "Juan@marmol.com",
                        Phone = "+5491154762312",
                        Address = "Peru 2464",
                        UserType = "Normal",
                        Money = 1234
                    },
                    new User
                    {
                        Name = "Franco",
                        Email = "Franco.Perez@gmail.com",
                        Phone = "+534645213542",
                        Address = "Alvear y Colombres",
                        UserType = "Premium",
                        Money = 112234
                    },
                    new User
                    {
                        Name = "Agustina",
                        Email = "Agustina@gmail.com",
                        Phone = "+534645213542",
                        Address = "Garay y Otra Calle",
                        UserType = "SuperUser",
                        Money = 112234
                    },
                };

                _applicationDbContext.Users.AddRange(users);
            }

            await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //TODO: log error
            throw;
        }
    }
}