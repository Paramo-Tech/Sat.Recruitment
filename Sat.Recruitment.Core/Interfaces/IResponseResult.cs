using Sat.Recruitment.Core.DataTransferObjects;

namespace Sat.Recruitment.Core.Interfaces
{
    public interface IResponseResult<T>
    {
        bool IsSuccess { get; set; }
        IOperationDto? Operation { get; set; }
        T Result { get; set; }
    }
}