using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace Sat.Recruitment.API.Extentions
{
    [ExcludeFromCodeCoverage]
    internal static class SwaggerExtension
    {
        internal static void AddSwaggerDocumentation(this IServiceCollection services) => services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("V1", GetOpenApiInfo());
            options.AddSecurityDefinition("Bearer", GetOpenApiSecurityScheme());
            options.AddSecurityRequirement(GetOpenApiSecurityRequirement());
        });

        internal static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("./V1/swagger.json", "Cross Border API"));
        }

        static OpenApiInfo GetOpenApiInfo() => new()
        {
            Title = "Recuitment",
            Version = "V1",
            Description = ""
        };

        static OpenApiSecurityScheme GetOpenApiSecurityScheme() => new()
        {
            Description = "JWT Athorization header usuing the bearer scheme. \n\n" +
                            "Enter 'Bearer' [space] and then your token in the text input velow. \n" +
                            "Example: 'Bearer 12345abcdef'",
            Name = "Autorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        };

        static OpenApiSecurityRequirement GetOpenApiSecurityRequirement() => new()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                }, new List<string>()
            }
        };
    }
}
