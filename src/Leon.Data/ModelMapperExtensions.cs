using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NH = NHibernate;
using Leon.Core;

namespace Leon.Data
{
    public static class ModelMapperExtensions
    {
        public static void AddEntityMappings(this NH.Mapping.ByCode.ModelMapper mapper,
            string prefix, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetExportedTypes();


                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(EntityBase)))
                    {

                        Type entityMappingType = typeof(EntityMapping<>);
                        Type genericType = entityMappingType.MakeGenericType(new Type[] { type });

                        object mappingInstance;

                        try
                        {
                            mappingInstance = Activator.CreateInstance(genericType, prefix);
                        }
                        catch (Exception e)
                        {
                            throw new NH.MappingException("Unable to instantiate mapping class (see InnerException): " + genericType, e);
                        }

                        var mapping = mappingInstance as NH.Mapping.ByCode.IConformistHoldersProvider;
                        if (mapping == null)
                        {
                            throw new ArgumentOutOfRangeException("type", "The mapping class must be an implementation of IConformistHoldersProvider.");
                        }

                        mapper.AddMapping(mapping);

                    }
                }
            }
        }
    }
}
