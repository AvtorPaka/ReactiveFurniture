using FluentValidation;
using Management.Service.Domain.Models;

namespace Management.Service.Domain.Validators;

public class RegisterUserModelValidator: AbstractValidator<RegisterUserModel>
{
    public RegisterUserModelValidator()
    {
        RuleFor(m => m.Username)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(50);
        
        RuleFor(m => m.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);
        
        RuleFor(m => m.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(50);
    }
}