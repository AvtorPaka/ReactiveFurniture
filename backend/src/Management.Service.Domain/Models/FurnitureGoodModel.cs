namespace Management.Service.Domain.Models;

public record FurnitureGoodModel(
    long Id,
    string Name,
    decimal Price,
    DateTimeOffset ReleaseDate
);