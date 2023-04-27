using System;
using System.Runtime.Serialization;
using AutoMapper;
using NUnit.Framework;
using Sat.Recruitment.Application.Common.Mappings;
using Sat.Recruitment.Application.Users.Commnads;
using Sat.Recruitment.Application.Users.Models;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.UnitTests.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        // Arrange
        [TestCase(typeof(User), typeof(UserViewModel))]
        [TestCase(typeof(UserViewModel), typeof(CreateUserCommand))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = GetInstanceOf(source);

            // Act / Assert
            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) != null ? Activator.CreateInstance(type) : FormatterServices.GetUninitializedObject(type);
        }
    }
}
