namespace TheatricalPlayersRefactoringKata.Domain.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
