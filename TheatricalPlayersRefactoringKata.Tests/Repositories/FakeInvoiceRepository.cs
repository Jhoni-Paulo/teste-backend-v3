using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;

namespace TheatricalPlayersRefactoringKata.Tests.Repositories
{
    public class FakeInvoiceRepository : IInvoiceRepository
    {

        private List<InvoiceEntity> _invoiceList = new List<InvoiceEntity>();
        public Task CreateAsync(InvoiceEntity entity)
        {
            _invoiceList.Add(entity);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<InvoiceEntity>> GetAllAsync()
        {
            return Task.FromResult((IEnumerable<InvoiceEntity>) _invoiceList);
        }

        public Task<InvoiceEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(_invoiceList.FirstOrDefault(invoice => invoice.IdInvoice == id));
        }

        public Task RemoveAsync(InvoiceEntity entity)
        {
            _invoiceList.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
