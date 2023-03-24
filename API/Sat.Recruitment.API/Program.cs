using Application.Handlers;
using Microsoft.AspNetCore.Diagnostics;
using Sat.Recruitment.API.Extentions;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencys(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exceptionType = exceptionHandlerFeature?.Error;
        if (exceptionType != null && exceptionType.Message.Contains("GeneralError"))
        {   
            await Results.Problem(exceptionType.Message.Split("-").Last(), null, StatusCodes.Status400BadRequest, "Bad Input").ExecuteAsync(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = Text.Plain;
            await Results.Problem().ExecuteAsync(context);

        }
    });
});

app.UseUserRoutes();
app.Run();

