using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    [Table("invoice")]
    public class InvoiceEntity
    {
        private InvoiceEntity() { }
        public InvoiceEntity(string customer, List<PerformanceEntity> performances)
        {
            Customer = customer;
            Performances = performances;
        }
        [Column("id_invoice")]
        public int IdInvoice { get; private set; }
        [Column("custumer")]
        public string Customer { get; private set; }
        public List<PerformanceEntity> Performances { get; private set; }

        public void SetToTestIdInvoice(int id)
        {
            IdInvoice = id;
        }

    }
}
