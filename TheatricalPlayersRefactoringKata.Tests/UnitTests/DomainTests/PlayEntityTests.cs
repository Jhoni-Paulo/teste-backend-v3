using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Validations;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UnitTests.DomainTests
{
    public class PlayEntityTests
    {

        private readonly IValidator<PlayEntity> _playEntityValidator;
        public PlayEntityTests()
        {
            _playEntityValidator = new PlayValidator();
        }

        [Fact]
        [Trait("DomainTests", "PlayEntityValidation")]
        public void GivenAnInvalidInvoice()
        {            
            PlayEntity play = new PlayEntity("", "", 0, PlayType.Tragedy);

            var validationResult = _playEntityValidator.Validate(play);

            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("Name is required."));
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("Slug is required."));
            Assert.Contains(validationResult.Errors, erro => erro.ErrorMessage.Contains("Lines must be at least 1."));
        }

        [Fact]
        [Trait("DomainTests", "PlayEntityValidation")]
        public void GivenAnValidInvoice()
        {
            PlayEntity play = new PlayEntity("Hamlet", "hamlet", 4024, PlayType.Tragedy);

            var validationResult = _playEntityValidator.Validate(play);

            Assert.True(validationResult.IsValid);
        }
    }
}
