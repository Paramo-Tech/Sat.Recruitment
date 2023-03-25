using FluentValidation;
using Sat.Recruitment.DataAccess.Contract.OtherClass;
using Sat.Recruitment.Domain.Contract.OtherClass;
using Sat.Recruitment.Domain.Models.OtherClass;

namespace Sat.Recruitment.Application.Services.OtherClass
{
    //Just for let you know how I create the file structure and how to implement it with the structure
    public class ExtraClassService : ServiceBase<ExtraClass>, IExtraClassService
    {
        public ExtraClassService(IExtraClassDataAccess itemDataAccess, IValidator<ExtraClass> validator) : base(itemDataAccess, validator)
        {
        }
    }
}
