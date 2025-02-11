using Management.Service.Domain.Contracts.Dal.Containers;
using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Domain.Models;
using Management.Service.Domain.Services.Hasher;

namespace Management.Service.Domain.Mappers;

public static class ModelMappers
{
    public static GetFurnitureGoodsContainer MapModelToContainer(this GetFurnitureGoodModel model)
    {
        return new GetFurnitureGoodsContainer(
            Name: model.Name,
            PriceMinRange: model.PriceMinRange,
            PriceMaxRange: model.PriceMaxRange,
            ReleaseDateMinRange: DateOnly.FromDateTime(model.ReleaseDateMinRange.DateTime),
            ReleaseDateMaxRange: DateOnly.FromDateTime(model.ReleaseDateMaxRange.DateTime)
        );
    }

    public static UserCredentialEntity MapModelToEntity(this RegisterUserModel model)
    {
        return new UserCredentialEntity
        {
            Id = -1,
            Email = model.Email,
            Username = model.Username,
            Password = PasswordHasher.Hash(model.Password)
        };
    }
}