using Management.Service.Domain.Models;

namespace Management.Service.Domain.Services.Interfaces;

public interface IFurnitureGoodsService
{
    public Task<IReadOnlyList<FurnitureGoodModel>> GetFurniture(GetFurnitureGoodModel model, CancellationToken cancellationToken);
    public Task MockFurnitureGoods(int amount, CancellationToken cancellationToken);
}