using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Leon.Core.Exceptions
{
    /// <summary>
    /// 本系统异常都要继承的基类
    /// </summary>
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException() { }

        public BaseException(string message)
            : base(message) { }

        public BaseException(string message, Exception innerException)
            : base(message, innerException) { }

        public BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
