using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Api.Domain.Services;
using Sat.Recruitment.Api.Domain.Services.Contracts;
using Sat.Recruitment.DataAccess.Implementation;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Services;
using Sat.Recruitment.Services.Contracts;

namespace Sat.Recruitment.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository>(x =>
            {
                return new FileUserRepository(() =>
                {
                    const string filesUsersTxt = "/Files/Users.txt";
                    var path = $"{Directory.GetCurrentDirectory()}{filesUsersTxt}";
                    return path;
                });
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserBuilderDirectorService, UserBuilderDirectorDefaultService>();
            
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
