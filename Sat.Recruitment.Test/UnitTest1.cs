﻿using Moq;
using Sat.Recruitment.Application;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Aggregates;
using Sat.Recruitment.Domain.Exceptions;
using Sat.Recruitment.Domain.ValueObjects;
using Xunit;

namespace Sat.Recruitment.Test;

public class UnitTest1
{
    protected Mock<IUserRepository> Repository { get; private set; }
    private readonly UserCreator creator;

    protected void ShouldHaveSave(User user)
    {
        this.Repository.Verify(x => x.Save(user), Times.AtLeastOnce());
    }

    public UnitTest1()
    {
        this.Repository = new Mock<IUserRepository>();
        this.creator = new UserCreator(this.Repository.Object);
    }

    [Fact]
    public async Task ItShould_CreateUser_OK()
    {
        var request = new UserRequest() { Address = "4th Avenue 5567", Email = "jorge@lasalle.org", Money = "333", Name = "Jorge Paz", Phone = "+59182345678", UserType = "Normal" };

        var response = await creator.Execute(request);

        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Email, response.Email);
        Assert.Equal(request.Phone, response.Phone);
        Assert.Equal(request.Address, response.Address);
    }

    [Fact]
    public async Task ItShould_FailAt_CreateUser_DuplicateUser()
    {
        var request = new UserRequest() { Address = "4th Avenue 5567", Email = "jorge@lasalle.org", Money = "333", Name = "Jorge Paz", Phone = "+59182345678", UserType = "Normal" };
        var expectedUser = new User(new UserName(request.Name), new Email(request.Email), new Address(request.Address), new Phone(request.Phone), new UserType(request.UserType), new Money(request.Money));
        this.Repository.Setup(r => r.SearchBy(It.IsAny<Func<User, bool>>())).ReturnsAsync(expectedUser); // The user already exists

        var ex = await Assert.ThrowsAsync<DuplicateUserException>(async () => await creator.Execute(request));

        Assert.NotNull(ex);
        Assert.Equal($"The user with name={request.Name} and email={request.Email} already exists", ex.Message);
    }

    [Fact]
    public async Task ItShould_CreateUser_NoDuplicateUser()
    {
        var request = new UserRequest() { Address = "4th Avenue 5567", Email = "jorge@lasalle.org", Money = "333", Name = "Jorge Paz", Phone = "+59182345678", UserType = "Normal" };
        var expectedUser = new User(new UserName(request.Name), new Email(request.Email), new Address(request.Address), new Phone(request.Phone), new UserType(request.UserType), new Money(request.Money));
        this.Repository.Setup(r => r.SearchBy(It.IsAny<Func<User, bool>>())).ReturnsAsync((User)null); // The user already exists

        var response = await creator.Execute(request);

        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Email, response.Email);
        Assert.Equal(request.Phone, response.Phone);
        Assert.Equal(request.Address, response.Address);
    }
}
