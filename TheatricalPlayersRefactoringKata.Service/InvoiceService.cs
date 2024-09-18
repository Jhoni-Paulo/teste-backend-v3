using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Service.Interfaces;

namespace TheatricalPlayersRefactoringKata.Service
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IValidator<InvoiceEntity> _invoiceValidator;
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceService(IValidator<InvoiceEntity> invoiceValidator, IInvoiceRepository invoiceRepository)
        {
            _invoiceValidator = invoiceValidator;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<InvoiceEntity> CreateAsync(InvoiceEntity invoiceEntity)
        {
            try
            {
                var validationResult = _invoiceValidator.Validate(invoiceEntity);

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);

                return await _invoiceRepository.CreateAsync(invoiceEntity);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<InvoiceEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(InvoiceEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
