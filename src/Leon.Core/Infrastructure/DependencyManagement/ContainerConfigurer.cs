using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leon.Core.Configuration;

namespace Leon.Core.Infrastructure.DependencyManagement
{
    public class ContainerConfigurer
    {
        public virtual void Configure(IEngine engine, ContainerManager containerManager, CMSConfig configuration)
        {
            //other dependencies
            containerManager.AddComponentInstance<CMSConfig>(configuration, "Leon.CMS.configuration");
            containerManager.AddComponentInstance<IEngine>(engine, "Leon.CMS.engine");
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "Leon.CMS.containerConfigurer");

            //type finder
            containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("Leon.CMS.typeFinder");
            var typeFinder = containerManager.Resolve<ITypeFinder>();
            containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                //sort
                drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
                foreach (var dependencyRegistrar in drInstances)
                    dependencyRegistrar.Register(x, typeFinder);
            });
        }
    }
}
