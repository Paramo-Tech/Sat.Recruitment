using System.Threading.Tasks;
using NUnit.Framework;
using static Sat.Recruitment.Application.IntegrationTests.Testing;

namespace Sat.Recruitment.Application.IntegrationTests
{
    [TestFixture]
    public abstract class BaseTestFixture
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
