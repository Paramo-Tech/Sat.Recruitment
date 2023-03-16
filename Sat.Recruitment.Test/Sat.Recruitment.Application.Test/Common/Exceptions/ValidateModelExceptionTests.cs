using System;
using FluentAssertions;
using NUnit.Framework;
using Sat.Recruitment.Application.Common.Exceptions;

namespace Sat.Recruitment.Application.Test.Common.Exceptions
{
    public class ValidateModelExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidateModelException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

    }
}
