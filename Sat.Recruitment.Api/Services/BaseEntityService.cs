using Sat.Recruitment.Api.Helpers;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services
{
    public abstract class BaseEntityService<Entity> where Entity : new()
    {
        private FileHelper _fileHelper;
        protected abstract string FileName { get; }

        public BaseEntityService()
        {
            _fileHelper = new FileHelper(FileName);
        }

        protected abstract Entity FormatEntity(string[] lines);

        public abstract bool Create(Entity entity);


        public virtual IList<Entity> GetAll()
        {
            var entities = new List<Entity>();
            var lines = _fileHelper.ReadFromFile();
            foreach (var line in lines)
            {
                string[] fields = line.Split(',');
                entities.Add(FormatEntity(fields));
            }

            return entities;
        }
    }
}
