using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Infrastructure.DataAccess
{
    public static class FileContext
    {
        public static List<User> ListUsers { get; set; } = new List<User>();
    }
}
