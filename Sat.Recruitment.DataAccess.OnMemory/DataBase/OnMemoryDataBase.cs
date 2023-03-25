using Sat.Recruitment.Domain.Models.OtherClass;
using Sat.Recruitment.Domain.Models.Users;

namespace Sat.Recruitment.DataAccess.OnMemory.DataBase
{
    //Just and in memory databse to simulate a database functionality and to demostrate an OpenClose working (You can use SQL, MySql, OnMemory, etc, with Dapper, EF or other ORM)
    //Contract are in Sat.Recruitment.DataAccess
    public static class OnMemoryDataBase
    {
        public static List<User> Users { get; set; } = new List<User>();
        public static List<ExtraClass> ExtraClasses { get; set; } = new List<ExtraClass>();
    }
}