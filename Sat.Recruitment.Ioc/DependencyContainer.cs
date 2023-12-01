using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Presenter;
using Sat.Recruitment.UseCases;

namespace Sat.Recruitment.Ioc
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddSatRecruitmentDependencies(this IServiceCollection services)
        {
            services.AddUseCasesServices();
            services.AddPresenters();
            return services;
        }

    }
}