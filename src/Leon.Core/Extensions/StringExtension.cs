using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Leon.Core.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 字符串转化为int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt32(this String s)
        {
            int result;
            int.TryParse(s, out result);
            return result;
        }

        public static decimal? TryToDecimal(this String s)
        {
            decimal result;
            if (decimal.TryParse(s, out result))
                return result;
            else
                return null;
        }

        public static int? TryToInt32(this String s)
        {
            int result;
            if (int.TryParse(s, out result))
                return result;
            else
                return null;
        }

        /// <summary>
        /// 解决Linq动态模糊查询的问题，String自己带的Contains方法 被操作的对象不能为空
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool LinqLikeContains(this string s, string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return value.Contains(s);
        }

        /// <summary>
        /// 如果s不为空，返回value；否则返回s
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string WhenNotNullOrEmpty(this string s, string value)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            else
                return value;
        }

        /// <summary>
        /// 如果s不为空，调用 action
        /// </summary>
        /// <param name="s"></param>
        /// <param name="action"></param>
        public static void WhenNotNullOrEmptyDo(this string s, Action action)
        {
            if (string.IsNullOrEmpty(s) == false)
                action();
        }

        public static string TryToLower(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            else
                return s.ToLower();
        }

        /// <summary>
        /// 返回由separator分割的字符串数组。
        /// <remarks>返回值不包括含有空字符串的数组元素</remarks>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] SplitWithoutEmpty(this string str, string separator)
        {
            return str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        ///   <summary>   
        ///   去除HTML标记   王鍌 2011-11-16
        ///   </summary>   
        ///   <param   name="NoHTML">包括HTML的源码   </param>   
        ///   <returns>已经去除后的文字</returns>   
        public static string NoHTML(this string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");


            return Htmlstring.Trim();
        }

    }
}
