using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {

        public ChangePasswordCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} is not valid email.")
                .MaximumLength(12).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.OldPassword)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .MaximumLength(12)
               .WithMessage("{PropertyName} must not exceed 12 characters.");

            RuleFor(p => p.NewPassword)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .MaximumLength(12)
               .WithMessage("{PropertyName} must not exceed 12 characters.");
        }
    }

}
