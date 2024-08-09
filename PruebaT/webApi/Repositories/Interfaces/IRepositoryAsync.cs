namespace webApi.Repositories.Interfaces
{
    public interface IRepositoryAsync<T>:IDisposable where T : class
    {
        //establece que metodos se van a implementar
        Task<List<T>> GetAll();
        Task<T?> GetByID(int? id);
        Task<T> Insert(T entity);
        Task<T> Delete(int id);
        Task Update(T entity);
    }
}
