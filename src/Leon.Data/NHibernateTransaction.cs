using System;
using System.Collections.Generic;
using NH = NHibernate;
using Leon.Core.Repository;


namespace Leon.Data
{
    public class NHibernateTransaction : ITransaction
    {
        NH.ITransaction transaction;

        bool isOriginator = true;

        public NHibernateTransaction(NH.ISession session)
        {
            transaction = session.Transaction;

            if (transaction.IsActive)
                isOriginator = false; // The method that first opened the transaction should also close it
            else
                transaction.Begin();
        }

        #region ITransaction Members

        public void Commit()
        {
            if (isOriginator && !transaction.WasCommitted && !transaction.WasRolledBack)
                transaction.Commit();
        }

        public void Rollback()
        {
            if (!transaction.WasCommitted && !transaction.WasRolledBack)
                transaction.Rollback();
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (isOriginator)
            {
                Rollback();
                transaction.Dispose();
            }

        }

        #endregion
    }
}
