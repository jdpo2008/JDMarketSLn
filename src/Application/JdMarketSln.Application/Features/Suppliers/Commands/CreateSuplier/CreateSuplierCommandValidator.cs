using FluentValidation;
using JdMarketSln.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Suppliers.Commands.CreateSuplier
{
    public class CreateSuplierCommandValidator : AbstractValidator<CreateSuplierCommand>
    {
        public CreateSuplierCommandValidator()
        {
            RuleFor(p => p.BusinessName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.TaxIdentifier)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(15).WithMessage("{PropertyName} must not exceed 15 characters.");

            RuleFor(p => p.ContactEmail).EmailAddress().WithMessage("{PropertyName} not is a email adress valid.");

            RuleFor(p => p.ContactName)
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 15 characters.");

            RuleFor(p => p.ContactPhone)
               .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(p => p.Country)
              .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(p => p.City)
              .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(p => p.Adrress)
             .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            
        }

    }
}
