using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant.Core
{
    public class DependencyContainer
    {
        private UnityContainer container;

        private static readonly Lazy<DependencyContainer> _instance = new Lazy<DependencyContainer>(() => new DependencyContainer());
        public static DependencyContainer Instance { get { return _instance.Value; } }
        private DependencyContainer()
        {
            this.container = new UnityContainer();
        }

        public void RegisterInstance<Interface>(Interface instance)
        {
            this.container.RegisterInstance<Interface>(instance);
        }

        public void RegisterType<Interface,Type>()
            where Type : Interface
        {
            this.container.RegisterType<Interface, Type>();
        }

        public T Resolve<T>()
        {
            return this.container.Resolve<T>();
        }
    }
}
