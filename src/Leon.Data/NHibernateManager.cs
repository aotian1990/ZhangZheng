using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using NH = NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Util;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using Leon.Core.Configuration;
using System.Configuration;
using StackExchange.Profiling;
using StackExchange.Profiling.NHibernate.Drivers;
using StackExchange.Profiling.NHibernate;


namespace Leon.Data
{
    public class NHibernateManager
    {
        private const string CurrentSessionKey = "nhibernate.current_session";

        private static NHibernate.Cfg.Configuration configuration;
        private static NH.ISessionFactory sessionFactory;
        private static string connectionString;
        private static string providerName;
        private static CurrentSessionContextClass currentSessionContextClass = CurrentSessionContextClass.web;

        public static CurrentSessionContextClass SetCurrentSessionContextClass
        {
            set
            {
                currentSessionContextClass = value;
            }
        }

        public static string XmlmMpping = string.Empty;

        public static NH.ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
                    sessionFactory = configuration.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        static NHibernateManager()
        {
            //Configuration();
        }

        public static void Configuration()
        {

            configuration = new NH.Cfg.Configuration();

            var config = ConfigurationManager.GetSection("NopConfig") as NopConfig;

            //if (System.Configuration.ConfigurationManager.ConnectionStrings.Count > 0)
            //{
            //    System.Configuration.ConnectionStringSettings settings =
            //        System.Configuration.ConfigurationManager.ConnectionStrings[0];

            //    connectionString = settings.ConnectionString;
            //    providerName = settings.ProviderName;
            //}
            connectionString = config.ConnectionString();
            providerName = config.ProviderName();

            string dialect;
            string driver;
            GetDialectAndDriver(providerName, out dialect, out driver);

            configuration.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            configuration.SetProperty("dialect", dialect);
            configuration.SetProperty("connection.driver_class", driver);
            configuration.SetProperty("connection.connection_string", connectionString);
            configuration.SetProperty("current_session_context_class", currentSessionContextClass.ToString());
            //configuration.SetProperty("connection.release_mode", "on_close");
            configuration.SetProperty("show_sql", "true");
            //configuration.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(MiniProfilerSql2008ClientDriver).AssemblyQualifiedName);
        }

        public static void AddMappings(string appName, Assembly assembly)
        {
            ModelMapper mapper = new ModelMapper(new EntityModelInspector());
            mapper.BeforeMapClass +=
            (mi, t, map) => map.Table(appName + "_" + t.Name + "s");
            mapper.AddMappings(assembly.GetExportedTypes());
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            string mapxml = domainMapping.AsString();
            XmlmMpping = mapxml;
            configuration.AddDeserializedMapping(domainMapping, appName + "Mappings");
        }      

        public static void AddMapping(string appName, params Assembly[] assemblies)
        {
            //ModelMapper mapper = new ModelMapper(new EntityModelInspector());
            //foreach (var assembly in assemblies)
            //{
            //    mapper.AddMappings(assembly.GetExportedTypes());
            //}
            //HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            //string mapxml = domainMapping.AsString();
            //configuration.AddDeserializedMapping(domainMapping, appName + "Mappings");
            //mapper
            NH.Mapping.ByCode.ModelMapper mapper = new NH.Mapping.ByCode.ModelMapper(new EntityModelInspector());
            mapper.AddEntityMappings(appName + "_", assemblies);
            mapper.BeforeMapProperty += (mi, propertyPath, map) =>
            {
                var pname = propertyPath.LocalMember.Name;
                if (propertyPath.LocalMember.Name.ToLower() == "content")
                //if (typeof(string).Equals(propertyPath.LocalMember.GetPropertyOrFieldType()))
                {
                    map.Type(NH.NHibernateUtil.StringClob);
                }
                if (propertyPath.LocalMember.Name.ToLower() == "contenten")
                //if (typeof(string).Equals(propertyPath.LocalMember.GetPropertyOrFieldType()))
                {
                    map.Type(NH.NHibernateUtil.StringClob);
                }
            };
            var hbmMappings = mapper.CompileMappingForAllExplicitlyAddedEntities();
            string mapxml = hbmMappings.AsString();
            XmlmMpping = mapxml;
            configuration.AddDeserializedMapping(hbmMappings, appName + "Mappings");
        }       

        public static void UpdateSchema()
        {

            NH.Tool.hbm2ddl.SchemaUpdate su = new NH.Tool.hbm2ddl.SchemaUpdate(configuration);
            su.Execute(true, true);
        }

        public static NH.ISession GetCurrentSession()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                NH.ISession currentSession = context.Items[CurrentSessionKey] as NH.ISession;

                if (currentSession == null)
                {
                    currentSession = SessionFactory.OpenSession();
                    context.Items[CurrentSessionKey] = currentSession;
                }
                return currentSession;
            }
            else
            {
                BindContext();
                return SessionFactory.GetCurrentSession();
            }
        }     

        private static void BindContext()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Bind(SessionFactory.OpenSession());
            }
        }

        private static void UnBindContext()
        {
            if (CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Unbind(SessionFactory);
            }
        }

        public static void CloseCurrentSession()
        {
            if (SessionFactory.GetCurrentSession().IsOpen)
            {
                SessionFactory.GetCurrentSession().Close();
            }
        }

        public static void CloseSession()
        {
            HttpContext context = HttpContext.Current;
            NH.ISession currentSession = context.Items[CurrentSessionKey] as NH.ISession;

            if (currentSession == null)
            {
                return;
            }

            currentSession.Close();
            context.Items.Remove(CurrentSessionKey);
        }

        #region 打开一个新的Session
        public static NH.ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
        #endregion

        public static void CloseSessionFactory()
        {
            UnBindContext();
            if (sessionFactory != null)
            {
                SessionFactory.Close();
            }
        }


        private static void GetDialectAndDriver(string providerName, out string dialect, out string driver)
        {
            var objects = new[]
           {
               new {Name="DB2", Dialect="NHibernate.Dialect.DB2Dialect", Driver="NHibernate.Driver.DB2Driver"},
               new {Name="DB2400", Dialect="NHibernate.Dialect.DB2400Dialect", Driver="NHibernate.Driver.DB2400Driver"},

               new {Name="Firebird", Dialect="NHibernate.Dialect.FirebirdDialect", Driver="NHibernate.Driver.FirebirdDriver"},
               
               new {Name="MsSql2000", Dialect="NHibernate.Dialect.MsSql2000Dialect", Driver="NHibernate.Driver.SqlClientDriver"},
               new {Name="MsSql2005", Dialect="NHibernate.Dialect.MsSql2005Dialect", Driver="NHibernate.Driver.SqlClientDriver"},
               new {Name="MsSql2008", Dialect="NHibernate.Dialect.MsSql2008Dialect", Driver="NHibernate.Driver.SqlClientDriver"},
               new {Name="MsSqlCe", Dialect="NHibernate.Dialect.MsSqlCeDialect", Driver="NHibernate.Driver.SqlServerCeDriver"},

               new {Name="MySQL", Dialect="NHibernate.Dialect.MySQLDialect", Driver="NHibernate.Driver.MySqlDataDriver"},
               new {Name="MySQL5", Dialect="NHibernate.Dialect.MySQLDialect", Driver="NHibernate.Driver.MySqlDataDriver"},

               new {Name="Oracle8i", Dialect="NHibernate.Dialect.Oracle8iDialect", Driver="NHibernate.Driver.OracleClientDriver"},
               new {Name="Oracle9i", Dialect="NHibernate.Dialect.Oracle9iDialect", Driver="NHibernate.Driver.OracleClientDriver"},
               new {Name="Oracle10g", Dialect="NHibernate.Dialect.Oracle10gDialect", Driver="NHibernate.Driver.OracleClientDriver"},

               new {Name="PostgreSQL", Dialect="NHibernate.Dialect.PostgreSQLDialect", Driver="NHibernate.Driver.NpgsqlDriver"},
               new {Name="PostgreSQL81", Dialect="NHibernate.Dialect.PostgreSQL81Dialect", Driver="NHibernate.Driver.NpgsqlDriver"},
               new {Name="PostgreSQL82", Dialect="NHibernate.Dialect.PostgreSQL82Dialect", Driver="NHibernate.Driver.NpgsqlDriver"},
          
               new {Name="SQLite", Dialect="NHibernate.Dialect.SQLiteDialect", Driver="NHibernate.Driver.SQLite20Driver"}
           };

            var o = objects.Single(x => x.Name == providerName);

            if (o != null)
            {
                dialect = o.Dialect;
                driver = o.Driver;

                return;
            }

            dialect = String.Empty;
            driver = String.Empty;
            return;
        }
    }

    public enum CurrentSessionContextClass
    {
        managed_web,
        call,
        thread_static,
        web,
        wcf_operation
    }
}
