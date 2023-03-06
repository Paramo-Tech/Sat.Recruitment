using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Infrastructure.Persistence;

namespace Sat.Recruitment.Test;

public class TestHelper
{
    private readonly ApplicationDbContext applicationDbContext;
    public TestHelper()
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseInMemoryDatabase(databaseName: "TestUsersDbInMemory");

        var dbContextOptions = builder.Options;
        applicationDbContext = new ApplicationDbContext(dbContextOptions);
        // Delete existing db before creating a new one
        applicationDbContext.Database.EnsureDeleted();
        applicationDbContext.Database.EnsureCreated();
    }

    public ApplicationDbContext GetInMemoryDb()
    {
        return applicationDbContext;
    }
}
