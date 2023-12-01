using Sat.Recruitment.DTOs;
using Sat.Recruitment.Entities;
using Sat.Recruitment.Entities.Exceptions;
using Sat.Recruitment.UseCases.Services.UserBonus;
using Sat.Recruitment.UseCasesAbstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.UseCases.CreateUser
{
    public class UserIterator : IPostUserInputPort
    {
        private readonly IUserBonusService _userBonusService;
        private readonly IFileReaderService _fileReaderService;
        private readonly IPostUserOutputPort _postUserOutputPort;
        private readonly List<Entities.POCOs.User> _users = new List<Entities.POCOs.User>();


        public UserIterator(IUserBonusService userBonusService, IFileReaderService fileReaderService, IPostUserOutputPort postUserOutputPort)
        {
            _userBonusService = userBonusService;
            _fileReaderService = fileReaderService;
            _postUserOutputPort = postUserOutputPort;
        }

        public async Task Handle(UserDTO userDto)
        {
            try
            {
                var newUser = CreateUserFromDto(userDto);
                Enum.TryParse(newUser.UserType, true, out UserTypeEnum parsedaUserTypeMethod);

                decimal bonusMoney = _userBonusService.CalculateUserBonus(parsedaUserTypeMethod, newUser.Money);
                newUser.Money += bonusMoney;

                StreamReader reader = _fileReaderService.ReadUsersFromFile();

                NormalizeEmail(ref newUser);

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();

                    if (string.IsNullOrEmpty(line))
                    {
                        continue; 
                    }

                    var lineElements = line.Split(',');

                    if (lineElements.Length < 6)
                    {
                        continue;
                    }

                    var user = new Entities.POCOs.User
                    {
                        Name = lineElements[0].ToString(),
                        Email = lineElements[1].ToString(),
                        Phone = lineElements[2].ToString(),
                        Address = lineElements[3].ToString(),
                        UserType = lineElements[4].ToString(),
                        Money = decimal.Parse(lineElements[5])
                    };
                    _users.Add(user);
                }

                reader.Close();


                foreach (var user in _users)
                {
                    if (user.Email == newUser.Email || user.Phone == newUser.Phone)
                    {
                        throw new DuplicatedUserException();
                    }

                    if (user.Name == newUser.Name && user.Address == newUser.Address)
                    {
                        throw new DuplicatedUserException();
                    }
                }

                Debug.WriteLine("User Created");

                await _postUserOutputPort.Handle(new Result
                {
                    IsSuccess = true,
                    Errors = "User Created"
                });
            }
            catch
            {
                throw new Exception();
            }
        }

        private Entities.POCOs.User CreateUserFromDto(UserDTO userDto)
        {

            return new Entities.POCOs.User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = userDto.UserType,
                Money = decimal.Parse(userDto.Money)
            };
        }

        private void NormalizeEmail(ref Entities.POCOs.User newUser)
        {
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
