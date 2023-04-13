using Sat.Recruitment.Business.Services;
using System;
using Xunit;

namespace Sat.Recruitment.Business.Tests.UserServiceTests
{
    public class UserServiceTests
    {
        protected UserService Service { get; set; }        
        public UserServiceTests()
        {            
            Service = new UserService();
        }
    }
}
