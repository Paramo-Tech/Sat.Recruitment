using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Data
{
    public static class DataContext 
    {
        public static List<User> UserList { get; set; } = new List<User>();
    }
}
