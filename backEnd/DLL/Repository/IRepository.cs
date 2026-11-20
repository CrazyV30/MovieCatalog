namespace Dll.Repository
{
    public interface IRepository<T>
    {
        Task<bool> Add(T entity);
        bool Remove(T entity);
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<bool> ExistId(int id);
        Task<bool> Update(T entity);
        Task SaveChangesAsync();
    }
}
