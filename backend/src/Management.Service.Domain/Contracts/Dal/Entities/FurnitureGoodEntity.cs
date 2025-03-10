namespace Management.Service.Domain.Contracts.Dal.Entities;

public class FurnitureGoodEntity
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public DateOnly ReleaseDate { get; init; }
}