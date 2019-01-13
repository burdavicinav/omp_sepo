using NHibernate;
using System;

namespace obj_lib
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ISession _session;

        private ITransaction _transaction;

        public UnitOfWork()
        {
            BeginTransaction();
        }

        public void BeginTransaction()
        {
            _session = Module.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        public void BeginTransaction(ISession session)
        {
            _session = session;
            _transaction = _session.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();

            CloseTransaction();
        }

        public void Rollback()
        {
            _transaction.Rollback();

            CloseTransaction();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Commit();

                CloseTransaction();
            }
        }

        private void CloseTransaction()
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }
}