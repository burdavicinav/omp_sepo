namespace obj_lib.Repositories
{
    public interface IRepository<T> : IViewRepository<T>
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}