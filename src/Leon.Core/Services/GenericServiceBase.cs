using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leon.Core;
using Leon.Core.Repository;

namespace Leon.Core.Services
{
    public abstract class GenericServiceBase<T> : IGenericService<T> where T : EntityBase
    {
        protected IRepository<T> repository;

        public GenericServiceBase()
        {

        }

        public GenericServiceBase(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual T Get(int id)
        {
            return this.repository.Get(id);
        }

        public virtual T Load(int id)
        {
            return this.repository.Load(id);
        }

        public virtual int Save(T entity)
        {
            if (entity == null)
            {
                return -1;
            }
            return this.repository.Save(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                return;
            }
            this.repository.Update(entity);
        }

        public virtual void SaveOrUpdate(T entity)
        {
            if (entity == null)
            {
                return;
            }
            this.repository.SaveOrUpdate(entity);
        }

        public virtual IList<T> LoadAll()
        {
            return this.repository.Query().ToList();
        }

        public virtual IList<T> LoadAllWithPage(out long count, int pageIndex, int pageSize)
        {
            var result = this.repository.Query();
            count = result.LongCount();
            return result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
