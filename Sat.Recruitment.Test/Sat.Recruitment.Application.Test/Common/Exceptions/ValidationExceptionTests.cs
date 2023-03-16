using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;
using Sat.Recruitment.Application.Common.Exceptions;

namespace Sat.Recruitment.Application.Test.Common.Exceptions
{
    public class ValidationExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Test]
        public void SingleValidationFailureCreatesASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Name can not be empty."),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo("Name");
            actual["Name"].Should().BeEquivalentTo("Name can not be empty.");
        }

        [Test]
        public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("User", "User is duplicated"),
                new ValidationFailure("Email", "Name can not be empty."),
                new ValidationFailure("Email", "Email format is incorrect."),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo("User", "Email");
            actual["Email"].Should().BeEquivalentTo("Name can not be empty.", "Email format is incorrect.");
        }
    }
}
