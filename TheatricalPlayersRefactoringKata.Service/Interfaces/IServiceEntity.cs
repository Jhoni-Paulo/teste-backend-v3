namespace TheatricalPlayersRefactoringKata.Service.Interfaces
{
    public interface IServiceEntity<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
