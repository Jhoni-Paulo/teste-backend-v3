using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using static ApprovalTests.Scrubber.PdfScrubber;

namespace TheatricalPlayersRefactoringKata.Tests.Repositories
{
    public class FakePlayRepository : IPlayRepository
    {
        private readonly List<PlayEntity> _plays;
        public FakePlayRepository()
        {
            _plays = new List<PlayEntity>() {
                new PlayEntity("Hamlet", "hamlet", 4024, PlayType.Tragedy),
                new PlayEntity("As You Like It", "as-like", 2670, PlayType.Comedy),
                new PlayEntity("Othello", "othello", 3560, PlayType.Tragedy),
                new PlayEntity("Henry V", "henry-v", 3227, PlayType.History),
                new PlayEntity("King John", "john", 2648, PlayType.History),
                new PlayEntity("Richard III", "richard-iii", 3718, PlayType.History)
            };
        }
        public async Task<PlayEntity> CreateAsync(PlayEntity entity)
        {
            _plays.Add(entity);
            return entity;
        }

        public Task<IEnumerable<PlayEntity>> GetAllAsync()
        {
            return Task.FromResult((IEnumerable<PlayEntity>)_plays);
        }

        public Task<PlayEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(_plays.FirstOrDefault(play => play.IdPlay == id));
        }

        public Task<PlayEntity> GetPlayBySlugAsync(string slug)
        {
            try
            {
                var play = _plays.FirstOrDefault(p => p.Slug == slug);

                if (play == null) throw new ArgumentNullException("Play not found");

                return Task.FromResult(play);
            }
            catch (System.ArgumentNullException)
            {
                throw;
            }
        }

        public Task RemoveAsync(PlayEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
