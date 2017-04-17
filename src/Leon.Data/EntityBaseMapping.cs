using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leon.Core;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Leon.Data
{
    public class EntityBaseMapping<TEntity> : ClassMapping<TEntity> where TEntity : EntityBase
    {
        public EntityBaseMapping()
        {
            Id(x => x.Id, map => { map.Column(typeof(TEntity).Name+"Id"); map.Generator(Generators.Identity); });
            Version(x => x.Version, map => { });
            
        }

    }
}
