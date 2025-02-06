namespace Management.Service.Domain.Contracts.Dal.Entities;

public class FurnitureGoodEntity
{
    public long Id { get; init; }
    public decimal Price { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateOnly ReleaseDate { get; init; }
}