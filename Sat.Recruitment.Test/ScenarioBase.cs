using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Reflection;
using Sat.Recruitment.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Sat.Recruitment.Domain.Models;
using System.Text;
using Sat.Recruitment.Domain.Repository.Users;
using System.Threading.Tasks;
using System.Threading;

namespace Sat.Recruitment.Test
{
    public class ScenarioBase
    {
        protected readonly IUsersRepository _repository;

        public ScenarioBase()
        {
            var context = InitializeInMemoryContext();
            _repository = new UsersRepository(context);
        } 

        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(ScenarioBase)).Location;
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var env = config.GetValue<string>("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var host = Host.CreateDefaultBuilder()
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
             .ConfigureWebHostDefaults(webHostBuilder =>
             {
                 webHostBuilder
                     .UseTestServer()
                     .UseEnvironment(env)
                     .UseContentRoot(Path.GetDirectoryName(path))
                     .UseConfiguration(config)
                     .UseStartup<TestsStartUp>();
             })
             .Build();

            host.Start();
            var serv = host.GetTestServer();
            return serv;
        }

        private UsersContext InitializeInMemoryContext()
        {
            var context = TestHelper.GenerateInMemoryContext();
            var users = new List<User>
            {
                {new User{ Name = "test1", Email = "test1@mail.com", Address = "testAddress1", Money = 1234, Password = Encoding.ASCII.GetBytes("Pwd1"), Phone = "1234567", UserType = UserTypeEnum.NORMAL, IsActive = true} },
                {new User{ Name = "test2", Email = "test2@mail.com", Address = "testAddress2", Money = 5678, Password = Encoding.ASCII.GetBytes("Pwd2"), Phone = "4357898", UserType = UserTypeEnum.PREMIUM, IsActive = true} }
            };
            context.AddRange(users);
            context.SaveChangesAsync();
            return context;
        }
    }
}
