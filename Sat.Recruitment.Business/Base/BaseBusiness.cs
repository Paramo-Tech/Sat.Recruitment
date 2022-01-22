using Sat.Recruitment.DAccess.Base;
using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business.Base
{
    public abstract class BaseBusiness<T> : IBaseBusiness<T>
    {
        public IBaseDAccess<T> DataAccess;

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await this.DataAccess.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<T> Get(Func<T, bool> filter)
        {
            try
            {
                return await this.DataAccess.Get(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual async Task<Result> Create(T item)
        {
            try
            {
                // validate again in business layer to verify if all fields are properly filled
                string errors = string.Empty;
                if (!this.Validate(item, ref errors))
                    throw new Exception("Invalid model. " + errors);

                // validate if item already exist
                if (await this.DataAccess.Get(Filter(item)) != null)
                    throw new Exception("The item is duplicated");

                var result = this.DataAccess.Create(item);

                if (result)
                    return new Result()
                    {
                        IsSuccess = true,
                        Message = "Item Created"
                    };
                else
                    throw new Exception("Error, item was not created");

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public virtual bool Validate(T item, ref string errors)
        {
            throw new NotImplementedException();
        }

        public virtual Func<T, bool> Filter(T item)
        {
            throw new NotImplementedException();
        }

    }
}
