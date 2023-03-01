using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business.Base
{
    public interface IBaseBusiness<T>
    {
        Task<List<T>> GetAll();

        Task<T> Get(Func<T, bool> filter);

        Task<Result> Create(T item);

        bool Validate(T item, ref string errors);

        Func<T, bool> Filter(T item);

        Task<List<T>> GetListByFilter(Func<T, bool> filter);


    }

}
