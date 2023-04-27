using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Common.Models.Configurations;
using Sat.Recruitment.Infrastructure.Persistence;
using Sat.Recruitment.Infrastructure.Services;

namespace Sat.Recruitment.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (configuration.GetSection(ConfigurationsSetting.Name)?.GetValue<bool>(nameof(ConfigurationsSetting.UseInMemoryDb)) ?? false)
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("SatRecruitmentDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();

            if (env.IsDevelopment())
            {
                services.AddHostedService<DbContextInitialiserHostedService>();
            }

            services.AddTransient<IDateTime, DateTimeService>();

            services.AddAuthorization();

            return services;
        }

        private class DbContextInitialiserHostedService : IHostedService
        {
            private readonly IServiceProvider _serviceProvider;

            public DbContextInitialiserHostedService(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public async Task StartAsync(CancellationToken cancellationToken)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                    await initialiser.InitialiseAsync();
                    await initialiser.SeedAsync();
                }
            }

            public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        }
    }
}
