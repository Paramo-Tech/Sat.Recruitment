using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Command;
using Sat.Recruitment.Application.DepencyInjection;
using Sat.Recruitment.Application.Services.GifCalculator.Factory;
using Xunit;

namespace Sat.Recruitment.Test.Application
{
    public class ApplicationDepencyInjectionTests
    {
        [Fact]
        public void RegisterApplicationServices_WithoutParameters_RegistersServices()
        {
            // Arrange.
            var services = new ServiceCollection();

            // Act.
            services.RegisterApplicationServices();
            var provider = services.BuildServiceProvider();

            // Assert.
            Assert.NotNull(provider.GetRequiredService<IMapper>());
            Assert.NotNull(provider.GetRequiredService<IMediator>());
            Assert.NotNull(provider.GetRequiredService<IValidator<CreateUserCommand>>());
            Assert.NotNull(provider.GetRequiredService<IUserGifCalculatorFactory>());
        }

    }
}
