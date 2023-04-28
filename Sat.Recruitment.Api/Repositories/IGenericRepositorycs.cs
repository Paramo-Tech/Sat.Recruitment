using System.Collections;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Repositories
{
    public interface IGenericRepositorycs<T> where T : class
    {
        IEnumerable<T> GetAll();

    }
}
