using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public class Data : IData
    {
        private readonly ILogger<Data> _logger;
        public Data(ILogger<Data> logger)
        {
            this._logger = logger;
        }

    public bool Exist(UserDto user)
        {
            try{
                using (var context = new AppTestContext())
                {
                    if (context.User.Any(x => x.Email == user.Email))
                        return true;
                    if (context.User.Any(x => x.Phone == user.Phone))
                        return true;
                    if (context.User.Any(x => x.Name == user.Address && x.Address == user.Address))
                        return true;

                }
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

            return false;
            
        }

        public async Task Save(UserDto user)
        {
            try
            {
                using (var context = new AppTestContext())
                {
                    await context.User.AddAsync(new User
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Phone = user.Phone,
                        Address = user.Address,
                        UserType = user.UserType,
                        Money = user.Money
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
