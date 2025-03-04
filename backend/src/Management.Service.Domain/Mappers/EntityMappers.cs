using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Domain.Models;

namespace Management.Service.Domain.Mappers;

public static class EntityMappers
{
    public static FurnitureGoodModel MapEntityToModel(this FurnitureGoodEntity entity)
    {
        return new FurnitureGoodModel(
            Id: entity.Id,
            Price: entity.Price,
            Name: entity.Name,
            ReleaseDate: (entity.ReleaseDate.ToDateTime(new TimeOnly(0)).ToUniversalTime() <=
                          DateTimeOffset.MaxValue)
                ? new DateTimeOffset(entity.ReleaseDate.ToDateTime(new TimeOnly(0)))
                : DateTimeOffset.MinValue
        );
    }

    public static IReadOnlyList<FurnitureGoodModel> MapEntitiesToModels(this IEnumerable<FurnitureGoodEntity> entities)
    {
        return entities.Select(x => x.MapEntityToModel()).ToList();
    }
}