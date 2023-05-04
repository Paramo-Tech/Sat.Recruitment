using NLog;
using NLog.Web;
using Sat.Rec.Core.Services.Interfaces;
using Sat.Rec.Core.Services;
using Sat.Rec.Core;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddDIServices(builder.Configuration);
    builder.Services.AddTransient<IUserService, UserService>();

    //builder.Services.AddControllers();
    builder.Services.AddControllersWithViews(options => { options.SuppressAsyncSuffixInActionNames = false; });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

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

}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
