using HomeAccountant.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAccountant.Core.Configuration;
using HomeAccountant.Core.DataServices;
using HomeAccountant.Core.DataRepositories;

namespace HomeAccountant.Core
{
    public class Bootstrapper
    {
        public static void Init()
        {
            DependencyContainer.Instance.RegisterInstance<IConnectionConfiguration>(Configuration.Configuration.Instance);
            DependencyContainer.Instance.RegisterType<IHomeAccountantDataService, HomeAccountantSqlDataService>();
            DependencyContainer.Instance.RegisterType<IHomeAccountantRepository, HomeAccountantSqlDataRepository>();
        }
    }
}
