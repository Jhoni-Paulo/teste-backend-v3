using FluentValidation;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Validations;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UnitTests.DomainTests
{
    public class InvoiceEntityTests
    {
        private readonly IValidator<InvoiceEntity> _invoiceEntityValidator;
        private readonly IValidator<PerformanceEntity> _performanceEntityValidator;
        public InvoiceEntityTests()
        {
            _performanceEntityValidator = new PerformanceValidator();
            _invoiceEntityValidator = new InvoiceValidator(_performanceEntityValidator);
        }

        [Fact]
        [Trait("DomainTests", "InvoiceEntityValidation")]
        public void GivenAnInvalidInvoice()
        {
            List<PerformanceEntity> performanceList = null;

            InvoiceEntity invoice = new InvoiceEntity("", performanceList);

            var validationResult = _invoiceEntityValidator.Validate(invoice);

            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("Customer is required."));
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("Performances list cannot be null."));
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("Invoice must contain at least one performance."));

        }

        [Fact]
        [Trait("DomainTests", "InvoiceEntityValidation")]
        public void GivenAnValidInvoice_AndAnInvalidPerformanceList()
        {
            List<PerformanceEntity> performanceList = new List<PerformanceEntity>() {
                new PerformanceEntity("", 0),
                new PerformanceEntity("", 0),
                new PerformanceEntity("", 0)
            };

            InvoiceEntity invoice = new InvoiceEntity("BigCo", performanceList);

            var validationResult = _invoiceEntityValidator.Validate(invoice);

            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("PlayId is required."));
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("Audience must be at least 1."));
        }

        [Fact]
        [Trait("DomainTests", "InvoiceEntityValidation")]
        public void GivenAnValidInvoice()
        {
            List<PerformanceEntity> performanceList = new List<PerformanceEntity>
            {
                new PerformanceEntity("hamlet", 55),
                new PerformanceEntity("as-like", 35),
                new PerformanceEntity("othello", 40),
            };

            InvoiceEntity invoice = new InvoiceEntity("BigCo", performanceList);

            var validationResult = _invoiceEntityValidator.Validate(invoice);

            Assert.True(validationResult.IsValid);
        }
    }
}
