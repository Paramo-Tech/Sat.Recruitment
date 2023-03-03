using System.Threading;
using System.Threading.Tasks;
using Core.Baseclasses;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Core.CQS.Commands
{
    public class CreateNewUserDTO : IGenericApplicationRequest<CreateNewUserResponse>, IRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string userType { get; set; }
        public string money { get; set; }
    }

    public class CreateNewUserCommand : BaseCommand<CreateNewUserDTO, CreateNewUserResponse>
    {
        readonly IUserRepository userRepository;

        public CreateNewUserCommand(IUserRepository _repo)
        {
            userRepository = _repo;
        }

        public async override Task<CreateNewUserResponse> Handle(CreateNewUserDTO request, CancellationToken cancellationToken)
        {
            User newUser = CreateUserFromDTO(request);

            // -- Normalize, we could put this method in some logic layer instead in the entity, but just for the example is in the entity
            newUser.NormalizeEmail();

            // -- Business Validations
            var exists = await userRepository.Exists(request);
            if (exists)
            {
                return new CreateNewUserResponse()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }

            // -- Repository for get all items
            await userRepository.Add(newUser);
            return new CreateNewUserResponse()
            {
                IsSuccess = true
            };
        }

        private User CreateUserFromDTO(CreateNewUserDTO UserDTO)
        {
            User newUser = default;
            if (UserDTO.userType == UserTypes.Normal.ToString())
            {
                newUser = new NormalUser(UserDTO.money)
                {
                    Name = UserDTO.name,
                    Email = UserDTO.email,
                    Address = UserDTO.address,
                    Phone = UserDTO.phone,
                    UserType = UserDTO.userType,
                };
            }
            else if (UserDTO.userType == UserTypes.SuperUser.ToString())
            {
                newUser = new SuperUser(UserDTO.money)
                {
                    Name = UserDTO.name,
                    Email = UserDTO.email,
                    Address = UserDTO.address,
                    Phone = UserDTO.phone,
                    UserType = UserDTO.userType,
                };
            }
            else // premium
            {
                newUser = new PremiumUser(UserDTO.money)
                {
                    Name = UserDTO.name,
                    Email = UserDTO.email,
                    Address = UserDTO.address,
                    Phone = UserDTO.phone,
                    UserType = UserDTO.userType,
                };
            }

            return newUser;
        }
    }

    public class CreateNewUserResponse : CommonResponse
    {
        
    }
}
