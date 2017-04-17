using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Leon.Core;
using Leon.Core.Infrastructure;
using Leon.Core.Validation;
using FluentValidation;
using FluentValidation.Mvc;
using Leon.Data;
using Goceen.Website.Domain;
using StackExchange.Profiling;
using Goceen.Website.Services;

namespace Goceen.Website.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //initialize engine context
            EngineContext.Initialize(false);

            //initialize NHibernate
            NHibernateManager.Configuration();
            NHibernateManager.AddMappings("Goceen", typeof(Goceen.Website.Domain.Mapping.SysArticleMapping).Assembly);
            NHibernateManager.UpdateSchema();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = false;
            //fluent validation
            var factory = new AutofacValidatorFactory();
            ModelValidatorProviders.Providers
                .Add(new FluentValidationModelValidatorProvider(factory));
            DataAnnotationsModelValidatorProvider
                .AddImplicitRequiredAttributeForValueTypes = false;
            ValidatorOptions.ResourceProviderType = typeof(FluentResourcesCN);
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            FluentValidationModelValidatorProvider.Configure();
            SetInitAccount(EngineContext.Current.Resolve<ISysUserService>());

        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
                
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        #region 设置默认管理员账号
        private static void SetInitAccount(ISysUserService services)
        {
            const string account = "admin";
            var user = services.Get(account);           
            var now = DateTime.Now;
            if (user == null)
            {
                user = new SysUser
                {
                    Account = account,
                    Name = "管理员",
                    Password = MD5("ADMIN" + "123456" + now.ToLongDateString()),
                    IsEnabled = true,   
                    CreateTime = now
                };
                services.Save(user);
            }

        }
        private static string MD5(string key)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            var result = BitConverter.ToString(data).Replace("-", "");
            return result;
        }
        #endregion
    }
}
