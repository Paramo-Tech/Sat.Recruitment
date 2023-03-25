using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Services.OtherClass;
using Sat.Recruitment.Application.Services.Users;
using Sat.Recruitment.DataAccess.Contract.OtherClass;
using Sat.Recruitment.DataAccess.Contract.Users;
using Sat.Recruitment.DataAccess.OnMemory.Repositories;
using Sat.Recruitment.Domain.Contract.OtherClass;
using Sat.Recruitment.Domain.Contract.Users;
using Sat.Recruitment.Domain.Models.OtherClass;
using Sat.Recruitment.Domain.Models.Users;
using Sat.Recruitment.Domain.Validators.OtherClass;
using Sat.Recruitment.Domain.Validators.Users;

namespace Sat.Recruitment.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDEMOServices(this IServiceCollection services)
        {
            services.AddScoped<IUserDataAccess, UserRepository>();
            services.AddScoped<IExtraClassDataAccess, ExtraClassRepository>();

            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<ExtraClass>, ExtraClassValidator>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExtraClassService, ExtraClassService>();
        }
    }
}
