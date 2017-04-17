using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace Leon.Core.T4
{
    public class T4EntityInfo
    {

        public string EntityName { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public IEnumerable<PropertyInfo> Properties { get; private set; }

        public T4EntityInfo(Type modelType)
        {
            var @namespace = modelType.Namespace;
            if (@namespace == null)
            {
                return;
            }
            var index = @namespace.LastIndexOf('.') + 1;
            EntityName = @namespace.Substring(index, @namespace.Length - index);            
            
            Name = modelType.Name;
            var descAttributes = modelType.GetCustomAttributes(typeof(DescriptionAttribute), true);
            Description = descAttributes.Length == 1 ? ((DescriptionAttribute)descAttributes[0]).Description : Name;
            Properties = modelType.GetProperties();
        }
    }
}
