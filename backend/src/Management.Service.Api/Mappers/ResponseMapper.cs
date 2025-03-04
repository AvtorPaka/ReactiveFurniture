using Management.Service.Api.Contracts.Responses;
using Management.Service.Domain.Models;

namespace Management.Service.Api.Mappers;

internal static class ResponseMapper
{
    private static GetFurnitureGoodsResponse MapModelToResponse(this FurnitureGoodModel model)
    {
        return new GetFurnitureGoodsResponse(
            Id: model.Id,
            Price: model.Price,
            Name: model.Name,
            ReleaseDate: model.ReleaseDate
        );
    }

    internal static IReadOnlyList<GetFurnitureGoodsResponse> MapModelsToResponse(
        this IEnumerable<FurnitureGoodModel> models)
    {
        return models.Select(m => m.MapModelToResponse()).ToList();
    }
}