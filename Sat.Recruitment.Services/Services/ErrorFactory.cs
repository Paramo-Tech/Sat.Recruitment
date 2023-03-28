using Sat.Recruitment.Core.DataTransferObjects;
using Sat.Recruitment.Core.Interfaces;

namespace Sat.Recruitment.Services.Services
{
    public class ErrorFactory : IErrorFactory
    {
        public IResponseResult<string> CreateError(string method, Exception ex, string? message = null)
        {
            IResponseResult<string>? result;
            switch (ex.GetType().Name)
            {
                case "NotFoundException":
                case "UserNotFoundException":
                    result = new ResponseResultNotFound<string>(ex.Message, $"{method}-404");
                    break;
                case "UserDuplicatedException":
                case "ValidationException":
                    result = new ResponseResultBadRequest<string>(ex.Message, $"{method}-400");
                    break;
                default:
                    result = new ResponseResult<string>(false, ex.Message, new OperationDto() { Status = 500, Code = $"{method}-500", Message = "Internal error." });
                    break;
            }
            return result;
        }

    }
}
