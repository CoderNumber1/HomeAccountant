using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant.Core.Configuration
{
    public class Configuration : IConnectionConfiguration
    {
        private static readonly Lazy<Configuration> _instance = new Lazy<Configuration>(() => new Configuration());
        public static Configuration Instance { get { return _instance.Value; } }
        private Configuration() { }

        public string HomeAccountantConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["HomeAccountantContext"].ConnectionString; }
        }
    }
}
