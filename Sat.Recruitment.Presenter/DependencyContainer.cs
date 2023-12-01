using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.UseCasesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Presenter
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<IPostUserOutputPort, PostUserPresenter>();
            return services;
        }
    }
}
