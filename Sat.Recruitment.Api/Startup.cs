using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.DTOs.Requests;
using Sat.Recruitment.EF.Context;
using Sat.Recruitment.Services.Commands;
using Sat.Recruitment.Services.Commands.Imp;
using Sat.Recruitment.Services.Domain.MoneyIncrease;
using Sat.Recruitment.Services.Domain.MoneyIncrease.Imp;
using Sat.Recruitment.Services.Services;
using Sat.Recruitment.Services.Services.Imp;
using Sat.Recruitment.Validations;

namespace Sat.Recruitment.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            AddDbContext(services);
            AddServices(services);
            AddCommands(services);
        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=SatRecruitmentDB.db"));
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IValidator<UserCreateRequest>, UserCreateValidator>();
        }

        private void AddCommands(IServiceCollection services)
        {
            services.AddScoped<IUserExistsCommand, UserExistsCommand>();
            services.AddScoped<IGetUsersCommand, GetUsersCommand>();
            services.AddScoped<ICreateUserCommand, CreateUserCommand>();
            services.AddScoped<ICalculateMoneyIncreaseCommand, CalculateMoneyIncreaseCommand>();
            services.AddScoped<INormalizeEmailCommand, NormalizeEmailCommand>();
            services.AddScoped<ICalculateMoneyIncreaseFactory, CalculateMoneyIncreaseFactory>();
            services.AddScoped(typeof(CalculateMoneyIncreaseNormal));
            services.AddScoped(typeof(CalculateMoneyIncreaseSuperUser));
            services.AddScoped(typeof(CalculateMoneyIncreasePremium));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}