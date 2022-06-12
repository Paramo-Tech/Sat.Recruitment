using MediatR;
using Users.Domain;
using Users.Domain.UserGif;

namespace Users.Application.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly ICalculateUserGif calculateUserGif;
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(
            ICalculateUserGif calculateUserGif,
            IUserRepository userRepository)
        {
            this.calculateUserGif = calculateUserGif;
            this.userRepository = userRepository;
        }

        public Task<Unit> Handle(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            var newUser = CreateUserCommandMapper.Execute(createUserCommand);

            newUser.Money += this.calculateUserGif.Execute(newUser.UserType, newUser.Money);

            var existendUser = this.userRepository.Search(newUser);

            if (existendUser is not null)
            {
                throw new ApplicationException("The user is duplicated");
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
