using Moq.AutoMock;


namespace Sat.Recruitment.CommonTests.TestsBases
{
    public class ServiceTestsBase
    {
        protected readonly AutoMocker Mocker;
        public ServiceTestsBase()
        {
            Mocker = new AutoMocker();
        }
    }
}
