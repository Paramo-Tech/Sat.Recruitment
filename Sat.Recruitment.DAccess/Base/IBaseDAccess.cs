using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.DAccess.Base
{
    public interface IBaseDAccess<T>
    {
        Task<List<T>> GetAll();

        Task<T> Get(Func<T, bool> filter);

        Task<List<T>> GetListByFilter(Func<T, bool> filter);

        bool Create(T item);        

        Task<List<T>> MapFromReaderToList(StreamReader reader);

        T MapFromLineToObject(string line);

        string MapFromObjectToLine(T item);

        void LogDBError(string msg);

    }

}
