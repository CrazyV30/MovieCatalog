namespace backEnd.Repository
{
    public interface IRepository<T>
    {
        bool Add(T entity);
        bool Remove(T entity);
        List<T> GetAll();
        T? GetById(int id);
        bool ExistId(int id);
        bool Update(T entity); 
    }
}
