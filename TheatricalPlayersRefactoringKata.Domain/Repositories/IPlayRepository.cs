using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Repositories
{
    public interface IPlayRepository : IRepository<PlayEntity>
    {
        Task<PlayEntity> GetPlayBySlugAsync(string name);
    }
}
