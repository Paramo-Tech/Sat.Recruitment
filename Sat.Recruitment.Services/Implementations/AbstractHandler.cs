using System;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Domain.Domains;
using Sat.Recruitment.Services.Definitions;

namespace Sat.Recruitment.Services.Implementations
{
     abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        public virtual User Handle(User user)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(user);
            }
            else
            {
                return null;
            }
        }
    }
}
