using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leon.Core.Validation
{
    public class FluentResourcesCN
    {
        public static string notnull_error
        {
            get
            {
                return "'{PropertyName}'不能为空。";
            }
        }

        public static string equal_error
        {
            get
            {
                return "'{PropertyName}'应该和'{PropertyValue}'相等。";
            }
        }
        public static string regex_error
        {
            get
            {
                return "'{PropertyName}'的格式不正确。";
            }
        }
        public static string predicate_error
        {
            get
            {
                return "指定的条件不符合'{PropertyName}'。";
            }
        }
        public static string notempty_error
        {
            get
            {
                return "'{PropertyName}'的长度必须大于 0。";
            }
        }
        public static string lessthan_error
        {
            get
            {
                return "'{PropertyName}'必须小于'{ComparisonValue}'。";
            }
        }
        public static string lessthanorequal_error
        {
            get
            {
                return "'{PropertyName}'必须小于或等于'{ComparisonValue}'。";
            }
        }
        public static string length_error
        {
            get
            {
                return "'{PropertyName}'必须在{MinLength}到{MaxLength}字符，你已经输入了 {TotalLength} 字符。";
            }
        }

        public static string inclusivebetween_error
        {
            get
            {
                return "'{PropertyName}'必须在{From}和{To}之间， 你输入了{Value}。";
            }
        }
        public static string greaterthan_error
        {
            get
            {
                return "'{PropertyName}'必须大于'{ComparisonValue}'。";
            }
        }
        public static string greaterthanorequal_error
        {
            get
            {
                return "'{PropertyName}'必须大于或等于'{ComparisonValue}'。";
            }
        }
        public static string exclusivebetween_error
        {
            get
            {
                return "'{PropertyName}'必须在{From}和{To}(排除)之间，你输入了{Value}。";
            }
        }
        public static string exact_length_error
        {
            get
            {
                return "'{PropertyName}' 必须是{MaxLength}字符长度，你已经输入了{TotalLength}字符。";
            }
        }
        public static string email_error
        {
            get
            {
                return "'{PropertyName}'是一个不合法的电子邮件地址。";
            }
        }
    }
}
