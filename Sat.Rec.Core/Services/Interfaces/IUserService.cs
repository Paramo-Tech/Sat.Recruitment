using Sat.Rec.Core.Validation;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<ValidationResult<User>> Create(User user);

        Task<ValidationResult<User>> GetAll();

        Task<ValidationResult<User>> GetById(int id);

        Task<ValidationResult<User>> Update(User user);

        Task<ValidationResult<User>> Delete(int id);
    }
}
