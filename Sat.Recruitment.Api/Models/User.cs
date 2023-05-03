using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Models
{
    [Table("users")]
    public abstract class User : IUser
    {
        [JsonIgnore]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phone")]
        public string Phone { get; set; }
        
        [Column("usertype")]
        public UserType UserType { get; set; }

        [Column("money")]
        public decimal Money { get; set; }

        public abstract void CalculateAllocationToUser();

        public abstract string GetPrivilegesToUser(int id);

        public void ToTakeVacations()
        {
            throw new NotImplementedException();
        }
    }

    public class Normal : User
    {
        public Normal() { }
        public override void CalculateAllocationToUser()
        {
            if (base.Money > 100)
            {
                base.Money = base.Money + (base.Money * Convert.ToDecimal(0.12));
            }
            if (base.Money > 10 && base.Money <= 100)
            {
                base.Money = base.Money + (base.Money * Convert.ToDecimal(0.8));
            }
        }

        public override string GetPrivilegesToUser(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class SuperUser : User
    {
        public SuperUser() { }
        public override void CalculateAllocationToUser()
        {
            if (base.Money > 100)
            {
                base.Money = base.Money + (base.Money * Convert.ToDecimal(0.20));
            }
        }

        public override string GetPrivilegesToUser(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class Premium : User
    {
        public Premium() { }
        public override void CalculateAllocationToUser()
        {
            if (base.Money > 100)
            {
                base.Money = base.Money + (base.Money * 2);
            }
        }

        public override string GetPrivilegesToUser(int id)
        {
            throw new NotImplementedException();
        }
    }

    public enum UserType
    {
        Normal,
        SuperUser,
        Premium
    }

    public static class UserFactory
    {
        public static User CreateUser(string userType)
        {
            var typeName = $"{typeof(UserFactory).Namespace}.{userType}";
            var type = Type.GetType(typeName);
            return Activator.CreateInstance(type) as User;
        }
    }
}
