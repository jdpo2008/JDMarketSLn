using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.UserName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.PasswordHash)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MinimumLength(8).WithMessage("{PropertyName} must greather than 8 characters.")
               .MaximumLength(12).WithMessage("{PropertyName} must not exceed 12 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} must not a vali mail.");
        }
    }
}
