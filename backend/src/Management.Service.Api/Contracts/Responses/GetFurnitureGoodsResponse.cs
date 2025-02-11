namespace Management.Service.Api.Contracts.Responses;

public record GetFurnitureGoodsResponse(
    decimal Price,
    string Name,
    DateTimeOffset ReleaseDate
);