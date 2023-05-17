using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(50).WithMessage("{PropertyName} must not exceed 25 characters.");
        }
    }
}
