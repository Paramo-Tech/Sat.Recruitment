using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Extensions;
// using Sat.Recruitment.Api.Filters;
// using Sat.Recruitment.Application;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapPost("/users", async ([FromServices] UserCreator creator, [FromBody] UserRequest request) =>
//{
//    var user = await creator.Execute(request);
//    return Results.Created("/users", user);
//}); //.AddEndpointFilter<DomainExceptionFilter>();

//app.UseAuthorization();

app.MapControllers();

app.Run();

