using NHibernate;
using System.Linq;

namespace obj_lib.Repositories
{
    public interface IViewRepository<T>
    {
        T GetById(object id);

        ICriteria GetCriteria();

        IQueryable<T> GetQuery();
    }
}