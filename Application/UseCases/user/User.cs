using Application.Interfaces;
using Application.InterfacesApplication;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.user
{
    public class User : IUserUseCase
    {
        private readonly IUserDbContext _iUserDbContext;
        public User(IUserDbContext userDbContext)
        {
            _iUserDbContext = userDbContext;
        }

        public bool CreateUser(UserDomain user)
        {
            //validations


            //mapper user domain to user dto

            //create store repository

            return true;
        }
    }
}
