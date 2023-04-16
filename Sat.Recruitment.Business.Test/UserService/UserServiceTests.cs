using Moq;
using Sat.Recruitment.Business.Services;
using System.IO;
using TypeMock.ArrangeActAssert;

namespace Sat.Recruitment.Business.Tests.UserServiceTests
{
    public class UserServiceTests
    {
        protected UserService Service { get; set; }
        protected Mock<UserService> MockService { get; set; }
        protected Mock<ICurrentDirectoryProvider> MockCurrentDirectoryProvider { get; set; }
        protected Mock<FileStream> MockFileStream { get; set; }
        protected Mock<StreamReader> MockStreamReader { get; set; }


        public UserServiceTests()
        {
            MockCurrentDirectoryProvider = new Mock<ICurrentDirectoryProvider>();
            MockService = new Mock<UserService>();
            MockFileStream = new Mock<FileStream>();
            MockStreamReader = new Mock<StreamReader>();
            Service = new UserService();
        }
    }
}

