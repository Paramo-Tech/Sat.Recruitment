using MediatR;
using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Application.Users.Commands.CreateUser;

public record struct CreateUserCommand(
    string Name,
    string Email,
    string Address,
    string Phone,
    UserType UserType,
    decimal Money
    ) : IRequest<int>;
