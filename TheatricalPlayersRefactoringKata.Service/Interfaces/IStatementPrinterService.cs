namespace TheatricalPlayersRefactoringKata.Service.Interfaces
{
    public interface IStatementPrinterService
    {
        Task<string> PrintStatementAsync(int invoiceId);
        Task<string> GenerateXmlStatementAsync(int invoiceId);
    }
}
