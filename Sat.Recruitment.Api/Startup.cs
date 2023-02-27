using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Sat.Recruitment.Api.Models.Factory;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Extensions;
using Sat.Recruitment.Api.Cache;
using Sat.Recruitment.Api.Cache.Interfaces;
using Sat.Recruitment.Api.Repository.Interfaces;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api.Repository.Seeder;
using Sat.Recruitment.Api.Models.Validators;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.Services;

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
            services.AddHttpContextAccessor();

            services.AddControllers();

            // Register FV validators
            //services.AddValidatorsFromAssemblyContaining<Startup>(lifetime: ServiceLifetime.Transient);
            // Add FV to Asp.net
            services.AddFluentValidationAutoValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "My API", Version = "v1" });
                c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
            });

            services.AddFluentValidationRulesToSwagger();

            services.Configure<CacheConfiguration>(Configuration.GetSection("CacheConfiguration"));

            services.AddTransient<ICacheService, MemoryCacheService>();
            services.AddMemoryCache();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserFactory, UserFactory>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddTransient<IValidator<UserDTO>, UserValidator>();
            services.AddTransient<IUserSeeder, UserSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserSeeder userSeeder)
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
            app.SeedUsers(userSeeder);
        }
    }
}
