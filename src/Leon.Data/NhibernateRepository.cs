using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH = NHibernate;
using NHibernate.Linq;
using System.Linq.Dynamic;
using Leon.Core;
using Leon.Core.Repository;

namespace Leon.Data
{
    public class NhibernateRepository<T> : IRepository<T> where T : EntityBase
    {
        private NH.ISession session;
        private bool useNHibernateManager = true;

        /// <summary>
        /// 
        /// </summary>
        public NhibernateRepository()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public NhibernateRepository(NH.ISession session)
        {
            this.session = session;
            this.useNHibernateManager = false;
        }


        /// <summary>
        /// 
        /// </summary>
        public NH.ISession Session
        {
            get
            {
                if (!useNHibernateManager)
                {
                    return session;
                }
                else
                {
                    return NHibernateManager.GetCurrentSession();
                }
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="entity"></param>
        //public void Save<TEntity>(TEntity entity) where TEntity : EntityBase
        //{
        //    NH.ISession session = Session;

        //    session.Save(entity);
        //    session.Flush();
        //}

        public int Save(T entity)
        {

            NH.ISession session = Session;
            int id = Convert.ToInt32(session.Save(entity));
            session.Flush();
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            NH.ISession session = Session;

            session.Update(entity);
            session.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void SaveOrUpdate(T entity)
        {
            NH.ISession session = Session;

            session.SaveOrUpdate(entity);
            session.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            NH.ISession session = Session;

            session.Delete(entity);
            session.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            NH.ISession session = Session;

            return session.Get<T>(id);
        }


        public T Load(int id)
        {
            NH.ISession session = Session;

            return session.Load<T>(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Query()
        {

            NH.ISession session = Session;

            return session.Query<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITransaction BeginTransaction()
        {
            NH.ISession session = Session;

            return new NHibernateTransaction(session);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (this.session != null)
            {
                this.session.Flush();
                CloseSession();
            }
        }


        private void CloseSession()
        {
            session.Close();
            session.Dispose();
            session = null;
        }
    }
}
