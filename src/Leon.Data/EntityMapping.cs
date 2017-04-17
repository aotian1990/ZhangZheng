using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NH = NHibernate;
using NHibernate.Mapping.ByCode;
using Leon.Core;

namespace Leon.Data
{
    public class EntityMapping<TEntity> : NH.Mapping.ByCode.Conformist.ClassMapping<TEntity>
        where TEntity : EntityBase
    {
        public EntityMapping(string prefix)
        {
            try
            {

                Type type = typeof(TEntity);

                Table(prefix + type.Name + "s");

                //
                Lazy(false);

                PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetGetMethod() == null || prop.GetSetMethod() == null)
                    {
                        continue;
                    }

                    string propName = prop.Name;

                    if (propName == "Id")
                    {
                        //Id(x => x.Id)

                        var target = Expression.Parameter(type);
                        var getPropertyValue = Expression.Property(target, prop);

                        var o = this.GetType().GetMethod("Id2")
                            .MakeGenericMethod(prop.PropertyType)
                            .Invoke(this, new object[] { Expression.Lambda(getPropertyValue, target), });

                        continue;
                    }

                    if (propName == "Version")
                    {
                        //Version(x => x.Version)

                        var target = Expression.Parameter(type);
                        var getPropertyValue = Expression.Property(target, prop);

                        var o = this.GetType().GetMethod("Version2")
                            .MakeGenericMethod(prop.PropertyType)
                            .Invoke(this, new object[] { Expression.Lambda(getPropertyValue, target), });

                        continue;
                    }

                    if (prop.PropertyType.IsSubclassOf(typeof(EntityBase)))
                    {
                        //ManyToOne(x => x.Genre);

                        Type anotherType = prop.PropertyType;

                        var target = Expression.Parameter(type);
                        var getPropertyValue = Expression.Property(target, prop);

                        this.GetType().GetMethod("ManyToOne2").MakeGenericMethod(anotherType)
                            .Invoke(this, new object[] { 
                                Expression.Lambda(getPropertyValue, target), type, anotherType });

                        continue;
                    }

                    if (prop.PropertyType.IsGenericCollection()
                        && prop.PropertyType.GetGenericInterfaceTypeDefinitions().Contains(typeof(IList<>))
                        && prop.PropertyType.DetermineCollectionElementType().IsSubclassOf(typeof(EntityBase)))
                    {
                        //Bag(x => x.Alums);

                        Type anotherType = prop.PropertyType.DetermineCollectionElementType();

                        var target = Expression.Parameter(type);
                        var getPropertyValue = Expression.Property(target, prop);

                        if (GetRelation(type, anotherType).Equals("OneToMany"))
                        {

                            this.GetType().GetMethod("Bag2_OneToMany")
                               .MakeGenericMethod(anotherType)
                               .Invoke(this, new object[] { Expression.Lambda(getPropertyValue, target), 
                                    type, anotherType });
                        }
                        else
                        {
                            string tableName;
                            if (type.Name.CompareTo(anotherType.Name) < 0)
                            {
                                tableName = type.Name + "s" + anotherType.Name + "s";
                            }
                            else
                            {
                                tableName = anotherType.Name + "s" + type.Name + "s";
                            }

                            tableName = prefix + tableName;



                            this.GetType().GetMethod("Bag2_ManyToMany")
                                .MakeGenericMethod(anotherType)
                                .Invoke(this, new object[] { Expression.Lambda(getPropertyValue, target), 
                                    type, anotherType, tableName });
                        }

                        continue;
                    }

                    if (prop.PropertyType.IsGenericCollection()
                        && prop.PropertyType.GetGenericInterfaceTypeDefinitions().Contains(typeof(IDictionary<,>)))
                    {
                        //Map(x => x.Settings);

                        string tableName = prefix + type.Name + propName;

                        var target = Expression.Parameter(type);
                        var getPropertyValue = Expression.Property(target, prop);


                        Type keyType = prop.PropertyType.DetermineDictionaryKeyType();
                        Type valueType = prop.PropertyType.DetermineDictionaryValueType();

                        this.GetType().GetMethod("Map2")
                            .MakeGenericMethod(keyType, valueType)
                            .Invoke(this, new object[] { Expression.Lambda(getPropertyValue, target), 
                                    type, tableName });

                        continue;
                    }

                    if (prop.PropertyType.GetInterfaces().Contains(typeof(IValueObject)))
                    {
                        //Component(x => x.Address);

                        var target = Expression.Parameter(type);
                        var getPropertyValue = Expression.Property(target, prop);

                        this.GetType().GetMethod("Component2").MakeGenericMethod(prop.PropertyType)
                            .Invoke(this, new object[] { Expression.Lambda(getPropertyValue, target) });

                        continue;
                    }


                    if (true)
                    {
                        //Property(x => x.Name);

                        var target = Expression.Parameter(type);
                        var getPropertyValue = Expression.Property(target, prop);

                        this.GetType().GetMethod("Property2")
                            .MakeGenericMethod(prop.PropertyType)
                            .Invoke(this, new object[] { Expression.Lambda(getPropertyValue, target), });

                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Id2<TProperty>(Expression property)
        {
            Id((Expression<Func<TEntity, TProperty>>)property,
                 x =>
                 {
                     x.Column(typeof(TEntity).Name + "Id");
                     x.Generator(Generators.Identity);
                 });
        }

        public void Version2<TProperty>(Expression property)
        {
            Version((Expression<Func<TEntity, TProperty>>)property,
                 x => { });
        }

        public void Property2<TProperty>(Expression property)
        {
            Property((Expression<Func<TEntity, TProperty>>)property,
                x => { });
        }

        public void Component2<TComponent>(Expression property) where TComponent : class
        {
            Component((Expression<Func<TEntity, TComponent>>)property, ComponentMap<TComponent>.Mapping());
        }

        public void ManyToOne2<TProperty>(Expression property, Type type, Type anotherType) where TProperty : class
        {
            ManyToOne((Expression<Func<TEntity, TProperty>>)property,
                m => { m.Column(anotherType.Name + "Id"); });
        }

        public void Bag2_OneToMany<TElement>(Expression property,
            Type type, Type anotherType) where TElement : class
        {
            var property1 = (Expression<Func<TEntity, IList<TElement>>>)property;

            Expression<Func<TEntity, IEnumerable<TElement>>> property2 =
                Expression.Lambda<Func<TEntity, IEnumerable<TElement>>>(property1.Body, property1.Parameters);

            Bag(property2,
                cm =>
                {
                    cm.Cascade(Cascade.All);
                    cm.Inverse(true);
                    cm.Key(km => km.Column(type.Name + "Id"));
                },
                m =>
                {

                });
        }



        public void Bag2_ManyToMany<TElement>(Expression property, Type type, Type anotherType, string tableName) where TElement : class
        {
            var property1 = (Expression<Func<TEntity, IList<TElement>>>)property;

            Expression<Func<TEntity, IEnumerable<TElement>>> property2 =
                Expression.Lambda<Func<TEntity, IEnumerable<TElement>>>(property1.Body, property1.Parameters);

            Bag(property2,
                cm =>
                {
                    //cm.Cascade(Cascade.All); 
                    //cm.Inverse(true); 
                    cm.Lazy(CollectionLazy.NoLazy);
                    cm.Table(tableName);
                    cm.Key(km => km.Column(type.Name + "Id"));
                },
                m =>
                {
                    m.ManyToMany(mtm => mtm.Column(anotherType.Name + "Id"));
                });
        }

        public void Map2<TKey, TElement>(Expression property, Type type, string tableName) where TElement : class
        {
            Map((Expression<Func<TEntity, IDictionary<TKey, TElement>>>)property,
                cm =>
                {
                    cm.Key(km => { km.Column(type.Name + "Id"); });
                    cm.Lazy(CollectionLazy.NoLazy);
                    cm.Table(tableName);

                },
                m =>
                {

                });
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



    internal class ComponentMap<TComponent> where TComponent : class
    {
        public static void Property<TProperty>(IComponentMapper<TComponent> cm, string propName)
        {
            var target = Expression.Parameter(typeof(TComponent));

            cm.Property<TProperty>((Expression<Func<TComponent, TProperty>>)Expression.Lambda(
                                Expression.Property(target, propName), target));
        }

        public static Action<IComponentMapper<TComponent>> Mapping()
        {
            return cm =>
            {
                PropertyInfo[] props = typeof(TComponent).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    typeof(ComponentMap<TComponent>).GetMethod("Property")
                            .MakeGenericMethod(prop.PropertyType)
                            .Invoke(null, new object[] { cm, prop.Name });
                }
            };
        }


    }
}
