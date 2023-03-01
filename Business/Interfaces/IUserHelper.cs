using Sat.Recruitment.Business;
using Sat.Recruitment.Business.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Interfaces
{
    public interface IParser<T,K>
    {
        T Parse(K obj);
        K Unparse(T obj);
    }
}
