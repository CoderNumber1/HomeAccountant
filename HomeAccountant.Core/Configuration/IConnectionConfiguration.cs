using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant.Core.Configuration
{
    public interface IConnectionConfiguration
    {
        string HomeAccountantConnectionString { get; }
    }
}
