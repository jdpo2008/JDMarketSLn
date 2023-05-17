using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Accounts.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {

        public ResetPasswordCommandValidator()
        {
            RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .EmailAddress().WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.Token)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .WithMessage("{PropertyName} must not exceed 250 characters.");

            RuleFor(p => p.Password)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .WithMessage("{PropertyName} must not exceed 12 characters.");
        }
    }
}
