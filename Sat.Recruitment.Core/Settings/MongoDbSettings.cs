using Sat.Recruitment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionURI { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;
    }
}
