using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Interfaces
{
    public interface IMongoDbSettings
    {
        string ConnectionURI { get; set; }

        string DatabaseName { get; set; }

        string CollectionName { get; set; }
    }
}
