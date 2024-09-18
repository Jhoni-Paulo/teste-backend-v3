using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Service;
using TheatricalPlayersRefactoringKata.Service.Interfaces;
using TheatricalPlayersRefactoringKata.Tests.Repositories;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UnitTests.ServiceTests
{
    public class StatementPrinterServiceTests
    {
        private readonly IStatementPrinterService _statementPrinterService;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPlayRepository _playRepository;
        public StatementPrinterServiceTests()
        {
            _invoiceRepository = new FakeInvoiceRepository();
            _playRepository = new FakePlayRepository();
            _statementPrinterService = new StatementPrinterService(_invoiceRepository, _playRepository);                
        }

        [Fact]
        [Trait("ServiceTests", "StatementPrinterServiceValidation")]
        public async void GivenAnInvalidInvoiceId_ShouldThrowArgumentNullException_WhenInvoiceIdIsNotFound()
        {
            int invoiceId = 0;
            var exception = await Assert.ThrowsAsync<ArgumentNullException>( async () =>
                 await _statementPrinterService.PrintStatementAsync(invoiceId) );

            Assert.Equal("Value cannot be null. (Parameter 'Invoice not found')", exception.Message);
        }

        [Fact]
        [Trait("ServiceTests", "StatementPrinterServiceValidation")]
        public async void GivenAnValidInvoiceId_ShouldThrowArgumentNullException_WhenPlayIsNotFound()
        {
            List<PerformanceEntity> performanceIncorrectList = new List<PerformanceEntity>
            {
                new PerformanceEntity("incorrectHamlet", 0),
                new PerformanceEntity("incorretAs-like", 0),
                new PerformanceEntity("incorrectOthello", 0),
            };
            var invoiceEntity = new InvoiceEntity("BigCo", performanceIncorrectList);
            invoiceEntity.SetToTestIdInvoice(1);

            await _invoiceRepository.CreateAsync(invoiceEntity);

            int invoiceId = 1;
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                 await _statementPrinterService.PrintStatementAsync(invoiceId));

            Assert.Equal("Value cannot be null. (Parameter 'Play not found')", exception.Message);
        }

        [Fact]
        [Trait("ServiceTests", "StatementPrinterServiceValidation")]
        public async void GivenAnValidInvoiceAndPlays_ShouldReturnFormattedStatement()
        {
            int invoiceId = 2;
            var resultStatement = await _statementPrinterService.PrintStatementAsync(invoiceId);

            Assert.Contains("Statement for BigCo", resultStatement);
            Assert.Contains("Hamlet: $650.00 (55 seats)", resultStatement);
            Assert.Contains("As You Like It: $547.00 (35 seats)", resultStatement);
            Assert.Contains("Othello: $456.00 (40 seats)", resultStatement);
            Assert.Contains("Amount owed is $1,653.00", resultStatement);
            Assert.Contains("You earned 47 credits", resultStatement);
        }
    }
}
