using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Service.Interfaces;

namespace TheatricalPlayersRefactoringKata.Service
{
    public class StatementPrinterService : IStatementPrinterService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPlayRepository _playRepository;
        public StatementPrinterService(IInvoiceRepository invoiceRepository, IPlayRepository playRepository)
        {
            _invoiceRepository = invoiceRepository;
            _playRepository = playRepository;
        }
        public async Task<string> PrintStatementAsync(int invoiceId)
        {
            throw new NotImplementedException();
        }
    }
}
