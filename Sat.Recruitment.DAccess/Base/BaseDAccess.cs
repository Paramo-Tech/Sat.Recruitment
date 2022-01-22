using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Sat.Recruitment.DAccess.Base
{
    public abstract class BaseDAccess<T> : IBaseDAccess<T>
    {
        public string FileName { get; set; }


        public async Task<List<T>> GetAll()
        {
            var read = this.ReadFile();
            return await MapFromReaderToList(read);

        }

        public async Task<T> Get(Func<T, bool> filter)
        {
            var list = await this.GetAll();
            return list.Where(filter).FirstOrDefault();

        }



        public async Task<List<T>> GetListByFilter(Func<T, bool> filter)
        {
            var list = await this.GetAll();
            return list.Where(filter).ToList();

        }

        public bool Create(T item)
        {
            var result = false;
            try
            {
                // set object in a string separated by ','
                string line = MapFromObjectToLine(item);

                // append line to text file
                // ...

                result = true;

            }
            catch (Exception ex)
            {
                LogDBError(ex.Message);
                return result;
            }

            return result;

        }


        private StreamReader ReadFile()
        {
            try
            {
                var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                while (directory.Name != "Sat.Recruitment")
                {
                    directory = directory.Parent;
                }
                var path = directory.FullName + String.Format("/Sat.Recruitment.DAccess/Files/{0}.txt", this.FileName);

                FileStream fileStream = new FileStream(path, FileMode.Open);
                StreamReader reader = new StreamReader(fileStream);
                return reader;

            }
            catch (Exception ex)
            {
                LogDBError(ex.Message);
                throw ex;
            }


        }


        public virtual async Task<List<T>> MapFromReaderToList(StreamReader reader)
        {
            try
            {
                List<T> list = new List<T>();
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var item = MapFromLineToObject(line);
                    list.Add(item);
                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public virtual T MapFromLineToObject(string line)
        {
            throw new NotImplementedException();
        }

        public virtual string MapFromObjectToLine(T item)
        {
            throw new NotImplementedException();
        }

        public void LogDBError(string msg)
        {

        }

    }

}
