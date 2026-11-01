namespace backEnd.Repository
{
    public interface IRepository<T>
    {
        bool Add(T entity);
        bool Remove(T entity);
        List<T> GetAll();
        T? GetByName(string name);
        bool ExistName(string name);
    }
}
