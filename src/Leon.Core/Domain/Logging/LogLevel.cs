using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Domain.Logging
{
    public enum LogLevel
    {
        All = 0,
        Debug = 10,
        Information = 20,
        Warning = 30,
        Error = 40,
        Fatal = 50,
        Off = 7
    }
}
