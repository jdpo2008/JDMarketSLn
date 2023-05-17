using FluentValidation;

namespace NetCoreApiTemplate.Application.Features.Accounts.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {

        public ForgotPasswordCommandValidator()
        {
            RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .EmailAddress().WithMessage("{PropertyName} must not exceed 150 characters.");
        }
    }
}
