using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Repository
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        //void Save<TEntity>(TEntity entity) where TEntity : EntityBase;

        /// <summary>
        /// 保存实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Save(T entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 保存或更新实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void SaveOrUpdate(T entity) ;

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        ///  获得一个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Load(int id);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IQueryable<T> Query();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITransaction BeginTransaction();

    }
}
