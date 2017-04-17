﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Leon.Core.Exceptions
{
    /// <summary>
    /// 数据验证异常
    /// </summary>
    public class ValidationException : BaseException
    {
        public ValidationException() { }

        public ValidationException(string message)
            : base(message) { }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException) { }

        public ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
