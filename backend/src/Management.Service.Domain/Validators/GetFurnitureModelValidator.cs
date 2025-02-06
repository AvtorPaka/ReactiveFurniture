using FluentValidation;
using Management.Service.Domain.Models;

namespace Management.Service.Domain.Validators;

public class GetFurnitureModelValidator: AbstractValidator<GetFurnitureGoodModel>
{
    public GetFurnitureModelValidator()
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.PriceMinRange).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PriceMaxRange).GreaterThanOrEqualTo(0).GreaterThanOrEqualTo(x => x.PriceMinRange);
    }
}