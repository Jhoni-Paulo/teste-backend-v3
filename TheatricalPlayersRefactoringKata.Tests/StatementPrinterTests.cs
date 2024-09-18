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
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
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
