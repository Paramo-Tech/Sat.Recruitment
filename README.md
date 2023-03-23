# Sat.Recruitment - Paramo Challenge 

This solution was refactoring for Paramo Challenge using .NET 6, CQRS, DDD  and Solid principles

## Best practices

* CQRS Architecture.
* DDD Pattern
* SOLID Principles
* Unit tests
* Containerization
* EF Migrations
* Mediator pattern

## Features
* Refactor controller, using correct api rest verbs
* Split commands and queries
* Replace .txt database for real database and use migrations
* Change controller validations for fluent validations
* Refactor throw exception handler
* Solution separeted in layers (api, application, domain, infrastructure)

## Run application

Docker

1. Build image
`docker build --tag paramochallenge .`

2. Run image
`docker run --publish 5000:80 paramochallenge`

Terminal

1. Clone github repository
`git clone https://github.com/ahcruz/Sat.Recruitment.git`

2. Restore packages
`dotnet restore`

3. Go to startup project
`cd src\Sat.Recruitment.Api`

4. Run application
`dotnet run`