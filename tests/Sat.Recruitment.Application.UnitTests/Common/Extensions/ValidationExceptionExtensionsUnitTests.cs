using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Sat.Recruitment.Application.Common.Extensions;

namespace Sat.Recruitment.Application.UnitTests.Common.Extensions
{
    public class ValidationExceptionExtensionsUnitTests
    {
        [Fact]
        public void MustReturnErrorMessageOnToErrorMessage()
        {
            //arrange
            var exception = new FluentValidation.ValidationException(string.Empty, new List<ValidationFailure>()
            {
                new ValidationFailure()
                {
                    ErrorMessage = "The email is not valid"
                },
                new ValidationFailure()
                {
                    ErrorMessage = "The email is not valid"
                }
            });

            //act
            var result = exception.ToErrorMessage();

            //assert
            Assert.Equal("The email is not valid, The email is not valid", result);
        }
    }
}
