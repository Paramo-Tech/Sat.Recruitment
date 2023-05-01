using Sat.Recruitment.Povider.IProvider;
using Sat.Recruitment.Povider.Provider;
using Sat.Recruitment.Services.IServices;
using Sat.Recruitment.Services.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserProvider,UserProvider>();
builder.Services.AddTransient<IService, Service>();
builder.Logging.AddLog4Net("log4net.config");
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
