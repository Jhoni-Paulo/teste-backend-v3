using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Service;
using TheatricalPlayersRefactoringKata.Service.Interfaces;
using TheatricalPlayersRefactoringKata.Tests.Repositories;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly IStatementPrinterService _statementPrinterService;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPlayRepository _playRepository;

    public StatementPrinterTests()
    {
        _invoiceRepository = new FakeInvoiceRepository();
        _playRepository = new FakePlayRepository();
        _statementPrinterService = new StatementPrinterService(_invoiceRepository, _playRepository);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async void TestTextStatementExample()
    {
        var resultStatement = await _statementPrinterService.PrintStatementAsync(3);

        Approvals.Verify(resultStatement);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async void TestXmlStatementExample()
    {
        var resultStatement = await _statementPrinterService.GenerateXmlStatementAsync(3);

        Approvals.Verify(resultStatement);
    }
}
