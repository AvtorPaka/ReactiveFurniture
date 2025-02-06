using Management.Service.Domain.Contracts.Dal.Containers;
using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Domain.Contracts.Dal.Interfaces;

namespace Management.Service.Infrastructure.Dal.Repositories;

public class FurnitureGoodRepository: BaseRepository, IFurnitureGoodRepository
{
    public async Task<IReadOnlyList<FurnitureGoodEntity>> QueryFurniture(GetFurnitureGoodsContainer paramsContainer, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);

        return new List<FurnitureGoodEntity>
        {
            // new FurnitureGoodEntity()
            // {
            //     Name = paramsContainer.Name,
            //     Price = paramsContainer.PriceMaxRange,
            //     ReleaseDate = paramsContainer.ReleaseDateMinRange
            // }
        };
    }
}