using FluentValidation;
using Management.Service.Domain.Models;

namespace Management.Service.Domain.Validators;

public class LoginUserModelValidator: AbstractValidator<LoginUserModel>
{
    public LoginUserModelValidator()
    {
        RuleFor(m => m.Password).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(m => m.Email).NotNull().NotEmpty().MaximumLength(50);
    }
}