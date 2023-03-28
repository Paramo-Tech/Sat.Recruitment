using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sat.Recruitment.Api.Middlewares;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;
using Sat.Recruitment.Core.Settings;
using Sat.Recruitment.Kernel.Features.Users.CreateUserCommand;
using Sat.Recruitment.Kernel.Features.Users.GetUserQueryByIdCommand;
using Sat.Recruitment.Kernel.Features.Users.GetUsersQueryCommand;
using Sat.Recruitment.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Sat.Recruitment.Api
{
    /// <summary>
    /// Class to startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Mongo DbSettings
            var mongoConfig = Configuration.GetSection("MongoDB");
            var mongoSettings = new MongoDbSettings() { 
                CollectionName = mongoConfig.GetValue<string>("CollectionName"), 
                ConnectionURI = mongoConfig.GetValue<string>("ConnectionURI"), 
                DatabaseName = mongoConfig.GetValue<string>("DatabaseName") 
            };

            // Addning controllers
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Users API",
                    Version = "v1",
                    Description = "This is a refactor of Paramo's Recruitment Test. I'm using MongoDB as repository, CQRS as Pattern, Dependency Injection, Unit Test and Middleware Exceptions Handler.",
                    Contact = new OpenApiContact
                    {
                        Name = "Norman Munoz",
                        Email = "norman.munoz@gmail.com"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }); 
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //Add UserService in Memory
            //services.AddSingleton<IUserService>(new MemoryUserService());

            //Mongo UserService
            services.AddSingleton<IUserService>(new MongoUserService(mongoSettings));

            //Error factory service
            services.AddScoped<IErrorFactory, ErrorFactory>();

            //User to Calculate gift value
            services.AddSingleton<IUserCalculateGiftValue>(new UserCalculateGiftValue());

            //Add CommandHandlers
            services.AddTransient<IRequestHandler<CreateUserRequest, object>, CreateUserHandler>();
            services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidator>();

            services.AddTransient<IRequestHandler<GetUserByIdQueryRequest, IUser>, GetUserByIdQueryHandler>();

            services.AddTransient<IRequestHandler<GetUsersQueryRequest, List<IUser>>, GetUsersQueryHandler>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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

            app.UseMiddleware<ExceptionHandling>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
