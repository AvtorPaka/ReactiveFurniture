using Management.Service.Domain.Contracts.Dal.Containers;
using Management.Service.Domain.Models;

namespace Management.Service.Domain.Mappers;

public static class ModelMappers
{
    public static GetFurnitureGoodsContainer MapModelToContainer(this GetFurnitureGoodModel model)
    {
        return new GetFurnitureGoodsContainer(
            Name: model.Name,
            PriceMinRange: model.PriceMinRange,
            PriceMaxRange: model.PriceMaxRange,
            ReleaseDateMinRange: DateOnly.FromDateTime(model.ReleaseDateMinRange.DateTime),
            ReleaseDateMaxRange: DateOnly.FromDateTime(model.ReleaseDateMaxRange.DateTime)
        );
    }
}