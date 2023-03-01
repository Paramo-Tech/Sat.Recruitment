using Domain.Contracts;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string usersFilePath)
        {
            services.AddScoped<IUserRepository>(sp => new UserFileSystemRepository(usersFilePath));            
            return services;
        }
    }
}