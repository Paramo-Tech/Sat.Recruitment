using Users.Domain;
using Users.Domain.Specifications;
using Xunit;

namespace Users.UnitTest.Users.Domain.Specifications
{
    public class ExistingUserSpecificationTest
    {
        [Theory]
        [MemberData(nameof(DataProvider.TestCases), MemberType = typeof(DataProvider))]
        public void IsSatisfied_MasiveCasesWithExpectedResult(
            User user,
            User userToCompare,
            bool expectedResult)
        {
            ExistingUserSpecification specification = new (
                user.Name,
                user.Email,
                user.Address,
                user.Phone);

            bool result = specification.IsSatisfied(userToCompare);

            Assert.Equal(result, expectedResult);
        }
    }
}
