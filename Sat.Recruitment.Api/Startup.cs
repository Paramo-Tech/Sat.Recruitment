using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Application.DepencyInjection;
using Sat.Recruitment.Infrastructure.SqlServer;
using Sat.Recruitment.Infrastructure.SqlServer.DepencyInjection;
using Sat.Recruitment.Infrastructure.TextFile.DepencyInjection;
using Sat.Recruitment.Api.Options;
using System;
using Sat.Recruitment.Api.Extensions;

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
            services.AddControllers();
            services.AddSwaggerGen();

            ConfigureDependencies(services);
        }

        public void ConfigureDependencies(IServiceCollection services)
        {
            services.AddOptions<PersistenceOptions>();

            // Register services from Application layer.
            services.RegisterApplicationServices();

            // Configure persisting infrastructure.
            var persistenceType = Configuration.GetPersistenceType();
            if (persistenceType == PersistenceType.TextFile)
            {
                // Register types for TextFile repository.
                services.RegisterTextFileInfrastructure();
            }
            else if (persistenceType == PersistenceType.EntityFramework)
            {
                // Register types for EF Core and SqlServer repository.
                string connectionString = ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
                services.RegisterSqlServerInfrastructure(connectionString);
            }
            else
            {
                // At least one known persistence type must be configured.
                throw new InvalidOperationException("Invalid persistence provider. Please review your configuration and make sure the PersistenceOptions is configured appropiately.");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var persistenceType = Configuration.GetPersistenceType();
            if (persistenceType == PersistenceType.EntityFramework)
            {
                // Apply EF Core migrations on runtime.
                using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = serviceScope.ServiceProvider.GetService<SatRecruitmentDbContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ErrorHandlingMiddleware>();
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
