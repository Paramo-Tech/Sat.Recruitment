using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.IRepositories;
using Sat.Recruitment.Domain.Services;
using Sat.Recruitment.Persistence.Models;
using Sat.Recruitment.Persistence.Repositories;
using Sat.Recruitment.Web.Authentication;
using Sat.Recruitment.Web.Filters;

namespace Sat.Recruitment.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Here in the future is posible create one Unit Of work to handle all the services and repos together.
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSqlServer<SatRecruitmentDbContext>(Configuration.GetConnectionString("SatRecruitmentDatabase"));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvcCore(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<ValidationFilter>();
            });
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sat Recruitment", Version = "v1" });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Please for login use SuperUser, email(username) and password created with /user endpoint"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
                c.OperationFilter<InjectProducesResponseTypeFilter>();
            });

            services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("BasicAuthentication", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
