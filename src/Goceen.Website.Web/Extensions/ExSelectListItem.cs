using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leon.Core.Extensions;
using System.Reflection;

namespace Goceen.Website.Web.Extensions
{
    public static class ExSelectListItem
    {
        public static List<SelectListItem> ToSelectListItem(Type enumType)
        {
            return (from Enum value in Enum.GetValues(enumType)
                    select new SelectListItem
                    {
                       
                        Text = value.GetDescription(),
                        Value = ((int)Enum.ToObject(enumType,value)).ToString()
                    }).ToList();
        }

        public static List<SelectListItem> ToSelectListItem(Type enumType, string selectName)
        {
            return (from Enum value in Enum.GetValues(enumType)
                    select new SelectListItem
                    {
                        Text = value.GetDescription(),
                        Value = ((int)Enum.ToObject(enumType,value)).ToString(),
                        Selected = value.GetDescription() == selectName ? true : false
                    }).ToList();
        }


        

        public static string GetDescription(this Enum value)
        {

            EnumDisplayNameAttribute attribute = value.GetType()            
                .GetField(value.ToString())            
                .GetCustomAttributes(typeof(EnumDisplayNameAttribute), false)            
                .SingleOrDefault() as EnumDisplayNameAttribute;
            return attribute == null ? value.ToString() : attribute.DisplayName;
        }

        //public static string GetEnumCustomDescription(object e)
        //{
        //    //获取枚举的Type类型对象
        //    Type t = e.GetType();

        //    var f = t.GetFields()
        //        .Where(m => m.GetValue(m.Name) == e.ToString() && m.IsDefined(typeof(EnumDisplayNameAttribute), true))
        //        .Select(m => (m.GetCustomAttribute(typeof(EnumDisplayNameAttribute),true) as EnumDisplayNameAttribute).DisplayName);

        //    return f.ToString();

            ////获取枚举的所有字段
            //FieldInfo[] ms = t.GetFields();

            ////遍历所有枚举的所有字段
            //foreach (FieldInfo f in ms)
            //{
            //    if (f.Name != e.ToString())
            //    {
            //        continue;
            //    }

            //    //第二个参数true表示查找EnumDisplayNameAttribute的继承链
            //    if (f.IsDefined(typeof(EnumDisplayNameAttribute), true))
            //    {
            //        return
            //          (f.GetCustomAttributes(typeof(EnumDisplayNameAttribute), true)[0] as EnumDisplayNameAttribute)
            //            .DisplayName;
            //    }
            //}

            ////如果没有找到自定义属性，直接返回属性项的名称
            //return e.ToString();
        //}
    }


}