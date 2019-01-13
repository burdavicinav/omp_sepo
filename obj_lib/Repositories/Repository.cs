using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;

namespace obj_lib.Repositories
{
    public class Repository<T> : IRepository<T>, IViewRepository<T>, IDisposable
    {
        private ISession _session;

        public Repository()
        {
            _session = Module.OpenSession();
        }

        public Repository(ISession session)
        {
            _session = session;
        }

        public void Create(T entity)
        {
            _session.Save(entity);
        }

        public void Update(T entity)
        {
            _session.Update(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public T GetById(object id)
        {
            T obj = _session.Get<T>(id);
            //_session.Evict(obj);

            return obj;
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
            {
                //_session.Close();
                //_session.Dispose();
            }
        }
    }
}