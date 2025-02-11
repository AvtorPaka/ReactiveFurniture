namespace Management.Service.Domain.Contracts.Dal.Containers;

public record GetFurnitureGoodsContainer(
    string Name,
    decimal PriceMinRange,
    decimal PriceMaxRange,
    DateOnly ReleaseDateMinRange,
    DateOnly ReleaseDateMaxRange
);