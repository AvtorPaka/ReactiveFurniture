namespace Management.Service.Api.Contracts.Responses;

public record GetFurnitureGoodsResponse(
    long Id,
    string Name,
    decimal Price,
    DateTimeOffset ReleaseDate
);