namespace Management.Service.Domain.Models;

public record GetFurnitureGoodModel(
    string Name,
    decimal PriceMinRange,
    decimal PriceMaxRange,
    DateTimeOffset ReleaseDateMinRange,
    DateTimeOffset ReleaseDateMaxRange
);