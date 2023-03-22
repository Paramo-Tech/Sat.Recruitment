using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Extensions;
using Sat.Recruitment.Domain.Interfaces.Data.Repositories;
using Sat.Recruitment.Infrastructure.Data;
using Sat.Recruitment.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddApplicationServices();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddDbContext<SatDbContext>(opt => opt.UseInMemoryDatabase("SatDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
