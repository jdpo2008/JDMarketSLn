using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Commands.UserAuthenticated
{
    public class UserAuthenticatedCommandValidator  : AbstractValidator<UserAuthenticatedCommand>
    {
        public UserAuthenticatedCommandValidator()
        {

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .EmailAddress().WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MinimumLength(8).WithMessage("{PropertyName} must greather than 8 characters.")
               .MaximumLength(12).WithMessage("{PropertyName} must not exceed 12 characters.");
          
        }
    }
}
