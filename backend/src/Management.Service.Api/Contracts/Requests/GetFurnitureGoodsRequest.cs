namespace Management.Service.Api.Contracts.Requests;

public record GetFurnitureGoodsRequest(
    string? Name,
    decimal PriceMinRange,
    decimal PriceMaxRange,
    DateTimeOffset ReleaseDateMinRange,
    DateTimeOffset ReleaseDateMaxRange
);