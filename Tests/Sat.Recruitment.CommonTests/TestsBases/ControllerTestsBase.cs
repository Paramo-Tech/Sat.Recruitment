using Microsoft.AspNetCore.Http;
using Moq.AutoMock;
using Sat.Recruitment.Core;

namespace Sat.Recruitment.CommonTests.TestsBases
{
    public abstract class ControllerTestsBase<T> where T : ApiBaseController
    {
        protected readonly T Controller;
        protected readonly AutoMocker Mocker;

        protected ControllerTestsBase()
        {
            Mocker = new AutoMocker();

            var httpResponseMock = Mocker.GetMock<HttpResponse>(true);
            httpResponseMock.Setup(mock => mock.Headers).Returns(new HeaderDictionary());

            var httpRequestMock = Mocker.GetMock<HttpRequest>(true);

            var httpContextMock = Mocker.GetMock<HttpContext>(true);
            httpContextMock.Setup(mock => mock.Response).Returns(httpResponseMock.Object);
            httpContextMock.Setup(mock => mock.Request).Returns(httpRequestMock.Object);

            Controller = Mocker.CreateInstance<T>();
            Controller.ControllerContext.HttpContext = httpContextMock.Object;
        }
    }
}
