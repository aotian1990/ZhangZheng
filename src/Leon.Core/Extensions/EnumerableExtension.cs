using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Extensions
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// 将列表元素拼接成由splitter分隔的字符串
        /// </summary>
        /// <example>
        ///     拼接字符串：
        ///         <c>new List&lt;string&gt; { "aa", "bb", "cc" }.Montage(p => p, ","); // 返回："aa,bb,cc"</c>
        ///     拼接对象属性：
        ///         <c>new List&lt;string&gt; { "aa", "bbb", "c" }.Montage(p => p.Length.ToString(), ","); // 返回："2,3,1"</c>
        ///     拼接枚举值：
        ///         <c>new List&lt;DomainType&gt; { DomainType.GuanHao, DomainType.YaoJiKe }.Montage(p => ((int)p).ToString(), ","); // 返回："1,2"</c>
        ///     拼接枚举名：
        ///         <c>new List&lt;DomainType&gt; { DomainType.GuanHao, DomainType.YaoJiKe }.Montage(p => p.ToString(), ","); // 返回："GuanHao,YaoJiKe"</c>
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="toString">将列表元素转换为字符串的委托</param>
        /// <param name="splitter">分隔符（可为空）</param>
        /// <returns></returns>
        public static string Montage<T>(this IEnumerable<T> source, Func<T, string> toString, string splitter)
        {
            StringBuilder result = new StringBuilder();
            splitter = splitter ?? string.Empty;
            foreach (T item in source)
            {
                result.Append(toString(item));
                result.Append(splitter);
            }
            string resultStr = result.ToString();
            if (resultStr.EndsWith(splitter))
                resultStr = resultStr.Remove(resultStr.Length - splitter.Length, splitter.Length);
            return resultStr;
        }

        /// <summary>
        /// 从泛型IEnumerable创建一个泛型List，每个元素由converter进行类型转换。
        /// </summary>
        /// <example>
        ///     将枚举List转换为Int32 List：
        ///         <c>new DomainType[] { DomainType.GuanHao, DomainType.YaoJiKe }.ToList(p => (int)p); // 返回：List&lt;int&gt;</c>
        /// </example>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static List<TResult> ToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> converter)
        {
            List<TResult> result = new List<TResult>();
            foreach (TSource item in source)
            {
                result.Add(converter(item));
            }
            return result;
        }

        /// <summary>
        /// 将 TSource 转换为 Dictionary
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="ignoreDuplicateKey">是否忽略重复的Key</param>
        /// <returns></returns>
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, bool ignoreDuplicateKey)
        {
            Dictionary<TKey, TSource> result = new Dictionary<TKey, TSource>();
            foreach (TSource item in source)
            {
                TKey key = keySelector(item);
                if (ignoreDuplicateKey)
                {
                    if (result.ContainsKey(key) == false)
                        result.Add(key, item);
                }
                else
                {
                    result.Add(key, item);
                }
            }
            return result;
        }

        //public static void AddStartDateFilter(this ICollection<DataFilter> filters, string propName, DateTime startDate)
        //{
        //    filters.Add(new DataFilter { type = "date", field = propName, comparison = "egt", value = startDate.ToString("MM/dd/yyyy") });
        //}

        //public static void AddEndDateFilter(this ICollection<DataFilter> filters, string propName, DateTime endDate)
        //{
        //    filters.Add(new DataFilter { type = "date", field = "CreateTime", comparison = "lt", value = endDate.AddDays(1).ToString("MM/dd/yyyy") });
        //}

        /// <summary>
        /// 从当前集内移除指定集合中的所有元素，返回值是一个新列表。
        /// <example>
        /// <c>new int[] { 1, 3, 3, 5, 7 }.Except(new int[] { 3, 5 }, p => p.ToString(), t => t.ToString()) // 返回 { 1, 7 }</c>
        /// </example>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="sourceKeySelector"></param>
        /// <param name="otherKeySelector"></param>
        /// <returns></returns>
        public static List<TSource> Except<TSource, TOther>(this IEnumerable<TSource> source, IEnumerable<TOther> other,
            Func<TSource, string> sourceKeySelector, Func<TOther, string> otherKeySelector)
        {
            List<TSource> result = new List<TSource>();

            // 生成 other 字典
            IDictionary<string, TOther> otherDict = new Dictionary<string, TOther>();
            foreach (TOther item in other)
            {
                string key = otherKeySelector(item);
                if (otherDict.ContainsKey(key) == false)
                    otherDict.Add(key, item);
            }

            foreach (TSource s in source)
            {
                if (!otherDict.ContainsKey(sourceKeySelector(s)))
                    result.Add(s);
            }
            return result;
        }

        /// <summary>
        /// 将source分割成perSliceNum个元素大小的子列表，分别对子列表执行executer，再将每次executer返回的列表合并返回
        /// </summary>
        /// <example>
        ///     <c>IList&lt;string&gt; s = new int[] { 1, 2, 3, 4, 5 }.SliceExecute(2, subList => new string[] { "[" + subList.Montage(i => i.ToString(), " ") + "]" });  // s 将为 {"[1 2]", "[3 4]", "[5]" } </c>
        /// </example>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="perSliceNum">分割后的每个子列表的大小</param>
        /// <param name="executer">被执行的回调函数</param>
        /// <returns></returns>
        public static List<TResult> SliceExecute<TSource, TResult>(this IEnumerable<TSource> source, int perSliceNum, Func<IEnumerable<TSource>, IEnumerable<TResult>> executer)
        {
            int len = source.Count();
            List<TResult> result = new List<TResult>();

            for (int groupIndex = 0; groupIndex < Math.Ceiling(Convert.ToDecimal(len) / perSliceNum); groupIndex++)
            {
                var group = source.Skip(groupIndex * perSliceNum).Take(perSliceNum);
                IEnumerable<TResult> groupResult = executer(group);
                foreach (TResult gr in groupResult)
                {
                    result.Add(gr);
                }
            }

            return result;
        }

        /// <summary>
        /// 返回一个新的列表，使用Converter决定新列表里的元素
        /// </summary>
        /// <example>
        ///     <c>IList&lt;int&gt; list = new string[] { "a", "bb", "ccc" }.Create(t => t.Length); // list = { 1, 2, 3 }</c>
        /// </example>
        /// <typeparam name="TSource">源列表类型</typeparam>
        /// <typeparam name="TTarget">新列表类型</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="Converter">转换函数</param>
        /// <returns></returns>
        public static IList<TTarget> Create<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> Converter)
        {
            IList<TTarget> result = new List<TTarget>();
            foreach (TSource item in source)
            {
                result.Add(Converter(item));
            }
            return result;
        }

        /// <summary>
        /// 将 items 添加到 list 里
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="items"></param>
        public static void Add<T>(this ICollection<T> list, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                list.Add(item);
            }
        }
    }
}
