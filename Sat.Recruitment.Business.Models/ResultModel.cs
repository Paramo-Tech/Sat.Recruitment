using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Models
{
    public class ResultModel
    {
        public UserModel Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
