using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models
{
    public partial class Api
    {
        public int Id { get; set; }
        public string Apiuser { get; set; }
        public string Password { get; set; }
        public string EmailUser { get; set; }
    }
}
