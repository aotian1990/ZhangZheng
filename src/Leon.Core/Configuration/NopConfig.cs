using System;
using System.Configuration;
using System.Xml;
using System.Collections.Generic;

namespace Leon.Core.Configuration
{
    public partial class NopConfig : IConfigurationSectionHandler
    {
       

        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new NopConfig();
            var dynamicDiscoveryNode = section.SelectSingleNode("DynamicDiscovery");
            if (dynamicDiscoveryNode != null && dynamicDiscoveryNode.Attributes != null)
            {
                var attribute = dynamicDiscoveryNode.Attributes["Enabled"];
                if (attribute != null)
                    config.DynamicDiscovery = Convert.ToBoolean(attribute.Value);
            }

            var engineNode = section.SelectSingleNode("Engine");
            if (engineNode != null && engineNode.Attributes != null)
            {
                var attribute = engineNode.Attributes["Type"];
                if (attribute != null)
                    config.EngineType = attribute.Value;
            }

            var startupNode = section.SelectSingleNode("Startup");
            if (startupNode != null && startupNode.Attributes != null)
            {
                var attribute = startupNode.Attributes["IgnoreStartupTasks"];
                if (attribute != null)
                    config.IgnoreStartupTasks = Convert.ToBoolean(attribute.Value);
            }
            

            var themeNode = section.SelectSingleNode("Themes");
            if (themeNode != null && themeNode.Attributes != null)
            {
                var attribute = themeNode.Attributes["basePath"];
                if (attribute != null)
                    config.ThemeBasePath = attribute.Value;
            }
            return config;
        }

        /// <summary>
        /// In addition to configured assemblies examine and load assemblies in the bin directory.
        /// </summary>
        public bool DynamicDiscovery { get; private set; }

        /// <summary>
        /// A custom <see cref="IEngine"/> to manage the application instead of the default.
        /// </summary>
        public string EngineType { get; private set; }

        /// <summary>
        /// Specifices where the themes will be stored (~/Themes/)
        /// </summary>
        public string ThemeBasePath { get; private set; }

        /// <summary>
        /// Indicates whether we should ignore startup tasks
        /// </summary>
        public bool IgnoreStartupTasks { get; private set; }

        public string ConnectionString()
        {
            //return "server=qds116204588.my3w.com;database=qds116204588_db;uid=qds116204588;pwd=gc6193693"; 
            return "Data Source=|DataDirectory|Shapotou.db;Pooling=true";
        } 
                //value = "server=192.168.1.254;database=CompanyCI;uid=sa;pwd=nxcdm.6193693"; 
     
        public string ProviderName()
        {
            //return "MsSql2008"; 
            return "SQLite";
        } 
           //     value = "MsSql2008"; 
        

    }
}
