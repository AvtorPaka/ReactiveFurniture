using Management.Service.Domain.Contracts.Dal.Containers;
using Management.Service.Domain.Contracts.Dal.Entities;

namespace Management.Service.Domain.Contracts.Dal.Interfaces;

public interface IFurnitureGoodRepository : IDbRepository
{
    public Task<IReadOnlyList<FurnitureGoodEntity>> QueryFurniture(GetFurnitureGoodsContainer paramsContainer,
        CancellationToken cancellationToken);

    public Task AddFurniture(FurnitureGoodEntity[] goods, CancellationToken cancellationToken);
}