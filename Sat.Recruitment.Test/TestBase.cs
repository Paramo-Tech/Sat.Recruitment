using System.Threading.Tasks;
using NUnit.Framework;

namespace Sat.Recruitment.Test
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
