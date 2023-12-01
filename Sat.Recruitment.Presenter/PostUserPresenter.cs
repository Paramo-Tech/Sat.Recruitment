using Sat.Recruitment.DTOs;
using Sat.Recruitment.UseCasesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Presenter
{
    public class PostUserPresenter : IPostUserOutputPort, IPresenter<Result>
    {
        public Result Content { get; private set; } = new Result();

        public Task Handle(Result result)
        {
            Content = result;
            return Task.CompletedTask;
        }
    }
}
