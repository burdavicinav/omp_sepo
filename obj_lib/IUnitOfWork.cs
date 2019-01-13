using NHibernate;

namespace obj_lib
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        void BeginTransaction(ISession session);

        void Commit();

        void Rollback();
    }
}