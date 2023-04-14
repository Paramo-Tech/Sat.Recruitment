using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Maps;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api.ViewModel;
using Sat.Recruitment.Domain.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {

        protected IMapper SetAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            options.AddProfile(new UserProfile()));

            return config.CreateMapper();
        }


        [Fact]
        public void PostUserOk()
        {


            var viewModel = new UserViewModel()
            {
                Address = "Av. Juan G",
                Email = "mike@gmail.com",
                Money = 124,
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal"

            };
            var context = new ValidationContext(viewModel, null, null);
            var results = new List<ValidationResult>();

            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(UserViewModel), typeof(UserViewModel)), typeof(UserViewModel));

            var isModelStateValid = Validator.TryValidateObject(viewModel, context, results, true);

            Assert.True(isModelStateValid);
            var userController = new UsersController(SetAutoMapper(), new UserService(new UserRepository()));


            var result = (OkObjectResult)userController.CreateUser(viewModel).Result;

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Created", result.Value);

        }


        [Fact]
        public void PostUserBadViewModel()
        {
            var sut = new UserViewModel()
            {

                Email = "mike@gmail.com",
                Money = 124,
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal"

            };
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(UserViewModel), typeof(UserViewModel)), typeof(UserViewModel));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            Assert.False(isModelStateValid);

        }

        [Fact]
        public void PostUserBadRequestError()
        {

            var userController = new UsersController(SetAutoMapper(), new UserService(new UserRepository()));
            userController.ModelState.AddModelError("address_error", "addres is required");
            var mikeUser = new UserViewModel()
            {
                Address = null,

                Email = "mike@gmail.com",
                Money = 124,
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal"

            };


            var result = (BadRequestObjectResult)userController.CreateUser(mikeUser).Result;

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void UserControllerDuplicateTestSameMailOk()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("testSetting.json").Build();
           
            var repo = new UserRepository();
            repo.FileName = configuration["File"]; ;
            var userController = new UsersController(SetAutoMapper(), new UserService(repo));

            var agusUser = new UserViewModel()
            {
                Address = "Av. Juan G",
                Email = "Agustina@gmail.com",
                Money = 124,
                Name = "Agustina",
                Phone = "+349 1122354215",
                UserType = "Normal"

            };

            var result = (ObjectResult)userController.CreateUser(agusUser).Result;


            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
        }

        [Fact]
        public void UserControllerDuplicateTestSameMailLowerCaseOk()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("testSetting.json").Build();

            var repo = new UserRepository();
            repo.FileName = configuration["File"]; ;
            var userController = new UsersController(SetAutoMapper(), new UserService(repo));

            var agusUser = new UserViewModel()
            {
                Address = "Av. Juan G",
                Email = "agustina@gmail.com",
                Money = 124,
                Name = "Agustina",
                Phone = "+349 1122354215",
                UserType = "Normal"

            };

            var result = (ObjectResult)userController.CreateUser(agusUser).Result;


            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
        }

        [Fact]
        public void UserControllerDuplicateTestSameAddressAndNAmeOk()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("testSetting.json").Build();

            var repo = new UserRepository();
            repo.FileName = configuration["File"]; ;
            var userController = new UsersController(SetAutoMapper(), new UserService(repo));

            var agusUser = new UserViewModel()
            {
                Address = "Garay y Otra Calle",
                Email = "AgustinaDistinctMail@gmail.com",
                Money = 124,
                Name = "Agustina",
                Phone = "+349 1122354215",
                UserType = "Normal"

            };

            var result = (ObjectResult)userController.CreateUser(agusUser).Result;


            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);

        }

       

        [Fact]
        public void UserControllerDuplicateTestSamePhoneOk()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("testSetting.json").Build();

            var repo = new UserRepository();
            repo.FileName = configuration["File"]; ;
            var userController = new UsersController(SetAutoMapper(), new UserService(repo));

            var agusUser = new UserViewModel()
            {
                Address = "Av. Juan G Distinct Address",
                Email = "AgustinaDistintMail@gmail.com",
                Money = 124,
                Name = "Distinct Name Agustina",
                Phone = "+534645213542",
                UserType = "Normal"

            };

            var result = (ObjectResult)userController.CreateUser(agusUser).Result;


            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);

        }

    }
}
