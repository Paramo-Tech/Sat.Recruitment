using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Services.Users.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UsersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                //var result = _mapper.Map<UserDto>(await _mediator.Send(new GetUserByIdQuery(id), CancellationToken.None));

                //if (result == null)
                //    return NoContent();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, ex.Message, ex.StackTrace);
                throw new Exception(Messages.GenericError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<UserDto>(await _mediator.Send(new CreateUserCommand(userDto)));
                return this.CreatedAtAction(nameof(this.Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace, ex.InnerException);
                throw new Exception(Messages.GenericError);
            }
            //var errors = "";

            //ValidateErrors(name, email, address, phone, ref errors);

            //if (errors != null && errors != "")
            //    return new Result()
            //    {
            //        IsSuccess = false,
            //        Errors = errors
            //    };

            //var newUser = new User
            //{
            //    Name = name,
            //    Email = email,
            //    Address = address,
            //    Phone = phone,
            //    UserType = userType,
            //    Money = decimal.Parse(money)
            //};

            //if (newUser.UserType == "Normal")
            //{
            //    if (decimal.Parse(money) > 100)
            //    {
            //        var percentage = Convert.ToDecimal(0.12);
            //        //If new user is normal and has more than USD100
            //        var gif = decimal.Parse(money) * percentage;
            //        newUser.Money = newUser.Money + gif;
            //    }
            //    if (decimal.Parse(money) < 100)
            //    {
            //        if (decimal.Parse(money) > 10)
            //        {
            //            var percentage = Convert.ToDecimal(0.8);
            //            var gif = decimal.Parse(money) * percentage;
            //            newUser.Money = newUser.Money + gif;
            //        }
            //    }
            //}
            //if (newUser.UserType == "SuperUser")
            //{
            //    if (decimal.Parse(money) > 100)
            //    {
            //        var percentage = Convert.ToDecimal(0.20);
            //        var gif = decimal.Parse(money) * percentage;
            //        newUser.Money = newUser.Money + gif;
            //    }
            //}
            //if (newUser.UserType == "Premium")
            //{
            //    if (decimal.Parse(money) > 100)
            //    {
            //        var gif = decimal.Parse(money) * 2;
            //        newUser.Money = newUser.Money + gif;
            //    }
            //}


            //var reader = ReadUsersFromFile();

            ////Normalize email
            //var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            //var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            //aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            //newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

            //while (reader.Peek() >= 0)
            //{
            //    var line = reader.ReadLineAsync().Result;
            //    var user = new User
            //    {
            //        Name = line.Split(',')[0].ToString(),
            //        Email = line.Split(',')[1].ToString(),
            //        Phone = line.Split(',')[2].ToString(),
            //        Address = line.Split(',')[3].ToString(),
            //        UserType = line.Split(',')[4].ToString(),
            //        Money = decimal.Parse(line.Split(',')[5].ToString()),
            //    };
            //    _users.Add(user);
            //}
            //reader.Close();
            //try
            //{
            //    var isDuplicated = false;
            //    foreach (var user in _users)
            //    {
            //        if (user.Email == newUser.Email
            //            ||
            //            user.Phone == newUser.Phone)
            //        {
            //            isDuplicated = true;
            //        }
            //        else if (user.Name == newUser.Name)
            //        {
            //            if (user.Address == newUser.Address)
            //            {
            //                isDuplicated = true;
            //                throw new Exception("User is duplicated");
            //            }

            //        }
            //    }

            //    if (!isDuplicated)
            //    {
            //        Debug.WriteLine("User Created");

            //        return new Result()
            //        {
            //            IsSuccess = true,
            //            Errors = "User Created"
            //        };
            //    }
            //    else
            //    {
            //        Debug.WriteLine("The user is duplicated");

            //        return new Result()
            //        {
            //            IsSuccess = false,
            //            Errors = "The user is duplicated"
            //        };
            //    }
            //}
            //catch
            //{
            //    Debug.WriteLine("The user is duplicated");
            //    return new Result()
            //    {
            //        IsSuccess = false,
            //        Errors = "The user is duplicated"
            //    };
            //}

            //return new Result()
            //{
            //    IsSuccess = true,
            //    Errors = "User Created"
            //};
        }

        ////Validate errors
        //private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        //{
        //    if (name == null)
        //        //Validate if Name is null
        //        errors = "The name is required";
        //    if (email == null)
        //        //Validate if Email is null
        //        errors = errors + " The email is required";
        //    if (address == null)
        //        //Validate if Address is null
        //        errors = errors + " The address is required";
        //    if (phone == null)
        //        //Validate if Phone is null
        //        errors = errors + " The phone is required";
        //}
    }
    
}
