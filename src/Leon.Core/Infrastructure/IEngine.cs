using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leon.Core.Configuration;
using Leon.Core.Infrastructure.DependencyManagement;

namespace Leon.Core.Infrastructure
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// Initialize components and plugins in the  environment.
        /// </summary>
        /// <param name="config">Config</param>
        void Initialize(NopConfig config);

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>();
    }
}
