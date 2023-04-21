using System.Collections.Generic;
using System.Net;

namespace Sat.Recruitment.Api.Models.DTOs
{
    public class ResponseDto
    {
        public int Status { get; set; } = (int) HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; }
    }
}
