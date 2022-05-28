using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Api.Domain.Services;
using Sat.Recruitment.Api.Domain.Services.Contracts;
using Sat.Recruitment.ApplicationServices;
using Sat.Recruitment.ApplicationServices.Contracts;
using Sat.Recruitment.DataAccess.Contracts;
using Sat.Recruitment.DataAccess.Implementation;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Services;

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
            services.AddScoped<IUserTextLineValidator, UserTextLineValidator>();
            services.AddScoped<IUsersSourceStream, UsersFromFile>(x =>
            {
                const string filesUsersTxt = "/Files/Users.txt";
                return new UsersFromFile(() => $"{Directory.GetCurrentDirectory()}{filesUsersTxt}");
            });
            
            services.AddScoped<IUserRepository,StreamUserRepository>();
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}