using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Application.Extensions;
using Sat.Recruitment.Domain.Interfaces.Data.Repositories;
using Sat.Recruitment.Infrastructure.Data;
using Sat.Recruitment.Infrastructure.Data.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddApplicationServices();
builder.Services.AddHealthChecks();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddDbContext<SatDbContext>(opt => opt.UseSqlite("Data Source=LocalDatabase.db"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapHealthChecks("/healthcheck");

//TODO to improve EF Migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SatDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();
