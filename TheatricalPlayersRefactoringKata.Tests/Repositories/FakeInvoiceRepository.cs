using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;

namespace TheatricalPlayersRefactoringKata.Tests.Repositories
{
    public class FakeInvoiceRepository : IInvoiceRepository
    {
        private List<InvoiceEntity> _invoiceList;
        public FakeInvoiceRepository()
        {
            _invoiceList = new List<InvoiceEntity>() { 
                new InvoiceEntity(
                    "BigCo",
                    new List<PerformanceEntity>
                    {
                        new PerformanceEntity("hamlet", 55),
                        new PerformanceEntity("as-like", 35),
                        new PerformanceEntity("othello", 40),
                    }
                ),
                new InvoiceEntity(
                    "BigCo",
                    new List<PerformanceEntity>
                    {
                        new PerformanceEntity("hamlet", 55),
                        new PerformanceEntity("as-like", 35),
                        new PerformanceEntity("othello", 40),
                        new PerformanceEntity("henry-v", 20),
                        new PerformanceEntity("john", 39),
                        new PerformanceEntity("henry-v", 20)
                    }
                )
            };
            _invoiceList[0].SetToTestIdInvoice(2);
            _invoiceList[1].SetToTestIdInvoice(3);
        }

        
        public async Task<InvoiceEntity> CreateAsync(InvoiceEntity entity)
        {
           _invoiceList.Add(entity);
            return entity;
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
