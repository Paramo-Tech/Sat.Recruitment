using Sat.Recruitment.DAccess.Base;
using Sat.Recruitment.DAccess.Contracts;
using System;
using System.IO;
using Sat.Recruitment.Entities;

namespace Sat.Recruitment.DAccess
{
    public class UserDAccess : BaseDAccess<User>, IUserDAccess
    {
        public UserDAccess()
        {
            this.FileName = DataBaseFiles.Users.ToString();
            
        }

        public override User MapFromLineToObject(string line)
        {
            try
            {
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public override string MapFromObjectToLine(User item)
        {
            string line = item.Name + "," + item.Email + "," + item.Phone + "," + item.Address + ",";
            line = string.IsNullOrEmpty(item.UserType) ? line + "," : line + item.UserType + ",";
            line = line + item.Money.ToString() + ",";
            line = line[0..^1]; // remove last ,
            return line;
        }


    }

}
