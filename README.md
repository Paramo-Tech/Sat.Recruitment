# Paramo Challenge
This is a solution for Paramo Challenge using ASP.NET Core following the principles of Clean Architecture.

## Technologies
- ASP.NET Core 7
- Entity Framework Core 7
- MediatR
- AutoMapper
- FluentValidation
- NUnit, FluentAssertions, Moq & Respawn

## Getting Started

### Database configuration

For demonstration purposes, the solution is configured to use an in-memory database by default. However, if you would like to utilize SQL Server, you need to update the "appsettings.json" file in the "Sat.Recruitment.Api" directory:

`"UseInMemoryDatabase": false,`
Verify that the `DefaultConnection` connection string within appsettings.json points to a valid SQL Server instance.

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Arquitecture Overview

Sat.Recruitment.Domain
The domain layer will contain entities, enums, exceptions, interfaces, types, and logic that are specific to the domain layer.

Sat.Recruitment.Application
The application logic layer contains the core functionality of the system, relies on the domain layer, and is independent of other layers and projects. This layer defines interfaces that external layers can implement, enabling flexible and modular design.

Sat.Recruitment.Application.Integration.Test
The Application Integration Test layer includes tests that validate the functionality of the Application layer.

Sat.Recruitment.Infrastructure
This layer contains classes for accessing external resources such as databases, services, and so on. These classes should be based on interfaces defined within the application layer.

Sat.Recruitment.Api
This layer is a ASP.NET Core 7 web API. This layer depends on both the Application and Infrastructure layers.
