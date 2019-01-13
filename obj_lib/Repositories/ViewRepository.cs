using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;

namespace obj_lib.Repositories
{
    public class ViewRepository<T> : IViewRepository<T>, IDisposable
    {
        private ISession _session;

        public ViewRepository()
        {
            _session = obj_lib.Module.OpenSession();
        }

        public ViewRepository(ISession session)
        {
            _session = session;
        }

        public T GetById(object id)
        {
            return _session.Get<T>(id);
        }

        public ICriteria GetCriteria()
        {
            return _session.CreateCriteria(typeof(T));
        }

        public IQueryable<T> GetQuery()
        {
            return _session.Query<T>();
        }

        public void Dispose()
        {
            if (_session != null)
                _session.Dispose();
        }
    }
}