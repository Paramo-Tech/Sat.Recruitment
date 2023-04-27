using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sat.Recruitment.Api.Filters;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Common.Models.Configurations;
using Sat.Recruitment.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sat.Recruitment.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringsSetting>(configuration.GetSection(ConnectionStringsSetting.Name));
            services.Configure<ConfigurationsSetting>(configuration.GetSection(ConfigurationsSetting.Name));

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(), new MediaTypeApiVersionReader("x-api-version"));
            });
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options => options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true));
            services.ConfigureOptions<ConfigureSwaggerGenOptions>();

            return services;
        }

        private class ConfigureSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

            public ConfigureSwaggerGenOptions(
                IApiVersionDescriptionProvider apiVersionDescriptionProvider)
            {
                _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
            }

            public void Configure(string name, SwaggerGenOptions options)
            {
                Configure(options);
            }

            public void Configure(SwaggerGenOptions options)
            {
                foreach (var apiVersionDescription in _apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(apiVersionDescription.GroupName,
                        new OpenApiInfo
                        {
                            Description = apiVersionDescription.IsDeprecated ? "This version has been deprecated. We suggest you upgrade to a newer one." : string.Empty,
                            Title = "Sat Recruitment API",
                            Version = apiVersionDescription.ApiVersion.ToString()
                        });
                }
            }
        }
    }
}
