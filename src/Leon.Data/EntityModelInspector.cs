using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NH = NHibernate;
using NHibernate.Mapping.ByCode;

namespace Leon.Data
{
    public class EntityModelInspector : NH.Mapping.ByCode.ExplicitlyDeclaredModel
    {

        public override bool IsOneToMany(MemberInfo member)
        {
            if (IsBag(member))
            {
                PropertyInfo p = member as PropertyInfo;

                Type anotherType = p.PropertyType.DetermineCollectionElementType();

                if (GetRelation(member.ReflectedType, anotherType).Equals("OneToMany"))
                {
                    return true;
                }
            }
            return base.IsOneToMany(member);
        }

        public override bool IsManyToMany(System.Reflection.MemberInfo member)
        {
            if (IsBag(member))
            {
                PropertyInfo p = member as PropertyInfo;

                Type anotherType = p.PropertyType.DetermineCollectionElementType();

                if (GetRelation(member.ReflectedType, anotherType).Equals("ManyToMany"))
                {
                    return true;
                }
            }

            return base.IsManyToMany(member);
        }

        private string GetRelation(Type type, Type anotherType)
        {
            PropertyInfo[] anotherProps = anotherType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in anotherProps)
            {
                if (prop.PropertyType.IsGenericCollection()
                   && prop.PropertyType.DetermineCollectionElementType().Equals(type))
                {
                    return "ManyToMany";
                }

            }

            return "OneToMany";
        }

    }
}
