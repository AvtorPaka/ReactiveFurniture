using Management.Service.Api.Contracts.Requests;
using Management.Service.Domain.Models;

namespace Management.Service.Api.Mappers;

internal static class RequestMapper
{
    internal static GetFurnitureGoodModel MapRequestToModel(this GetFurnitureGoodsRequest request)
    {
        return new GetFurnitureGoodModel(
            Name: request.Name?.Trim() ?? string.Empty,
            PriceMinRange: (request.PriceMinRange >= 0 && request.PriceMaxRange >= request.PriceMinRange)
                ? request.PriceMinRange
                : 0,
            PriceMaxRange: (request.PriceMaxRange >= 0 && request.PriceMaxRange >= request.PriceMinRange)
                ? request.PriceMaxRange
                : decimal.MaxValue,
            ReleaseDateMinRange: request.ReleaseDateMinRange,
            ReleaseDateMaxRange: request.ReleaseDateMaxRange
        );
    }

    internal static RegisterUserModel MapRequestToModel(this RegisterRequest request)
    {
        return new RegisterUserModel(
            Username: request.Username ?? "",
            Email: request.Email ?? "",
            Password: request.Password ?? ""
        );
    }

    internal static LoginUserModel MapRequestToModel(this LoginRequest request)
    {
        return new LoginUserModel(
            UserCred: request.UserCred ?? "",
            Password: request.Password ?? ""
        );
    }
}