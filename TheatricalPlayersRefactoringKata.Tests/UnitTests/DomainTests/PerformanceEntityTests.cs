using FluentValidation;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Validations;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UnitTests.DomainTests
{
    public class PerformanceEntityTests
    {
        private readonly IValidator<PerformanceEntity> _performanceEntityValidator;
        public PerformanceEntityTests()
        {
            _performanceEntityValidator = new PerformanceValidator();
        }

        [Fact]
        [Trait("DomainTests", "PerformanceEntityValidation")]
        public void GivenAnInvalidPerformance()
        {
            PerformanceEntity performance = new PerformanceEntity("", 0);

            var validationResult = _performanceEntityValidator.Validate(performance);

            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("PlayId is required."));
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("Audience must be at least 1."));
        }

        [Fact]
        [Trait("DomainTests", "PerformanceEntityValidation")]
        public void GivenAnValidPerformance()
        {
            PerformanceEntity performance = new PerformanceEntity("hamlet", 55);

            var validationResult = _performanceEntityValidator.Validate(performance);

            Assert.True(validationResult.IsValid);
        }
    }
}
