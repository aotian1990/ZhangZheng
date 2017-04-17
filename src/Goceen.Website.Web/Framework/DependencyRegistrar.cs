using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Leon.Core.Infrastructure.DependencyManagement;
using Goceen.Website.Domain;
using Goceen.Website.Services;
using Goceen.Website.Services.Implements;
using Leon.Core;
using Leon.Core.Cache;
using Leon.Core.Configuration;
using Leon.Core.Repository;
using Leon.Core.Infrastructure;
using Leon.Data;
using FluentValidation;


namespace Goceen.Website.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //repository
            builder.RegisterGeneric(typeof(NhibernateRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            builder.Register(c => (new HttpContextWrapper(HttpContext.Current) as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("cache_per_request").InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //services
            builder.RegisterType<SysArticleService>().As<ISysArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<SysCategoryService>().As<ISysCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<SysChannelService>().As<ISysChannelService>().InstancePerLifetimeScope();
            builder.RegisterType<SysMessageService>().As<ISysMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<SysIconService>().As<ISysIconService>().InstancePerLifetimeScope();
            builder.RegisterType<SysCarouselService>().As<ISysCarouselService>().InstancePerLifetimeScope();
            builder.RegisterType<SysNewsService>().As<ISysNewsService>().InstancePerLifetimeScope();
            builder.RegisterType<SysUserService>().As<ISysUserService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterType<SysConfigService>().As<ISysConfigService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterType<SysSlideService>().As<ISysSlideService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static"))
                .InstancePerLifetimeScope();

            AssemblyScanner.FindValidatorsInAssemblyContaining<Validators.ChannelValidator>()
             .ForEach(x => builder.RegisterType(x.ValidatorType).As(x.InterfaceType).SingleInstance()); 
        }

        public int Order
        {
            get { return 0; }
        }
    }

    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }
}