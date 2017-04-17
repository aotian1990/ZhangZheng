using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Extensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumDisplayNameAttribute : Attribute
    {
        public string DisplayName;

        public EnumDisplayNameAttribute(string name)
        {
            DisplayName = name;
        }
    }
}
