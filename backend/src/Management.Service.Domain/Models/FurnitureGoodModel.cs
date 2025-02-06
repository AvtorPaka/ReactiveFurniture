namespace Management.Service.Domain.Models;

public record FurnitureGoodModel(
    string Name,
    decimal Price,
    DateTimeOffset ReleaseDate
);