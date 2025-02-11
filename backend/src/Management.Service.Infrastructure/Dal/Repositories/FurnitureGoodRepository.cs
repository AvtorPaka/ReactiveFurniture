using Dapper;
using Management.Service.Domain.Contracts.Dal.Containers;
using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Domain.Contracts.Dal.Interfaces;
using Npgsql;

namespace Management.Service.Infrastructure.Dal.Repositories;

public class FurnitureGoodRepository : BaseRepository, IFurnitureGoodRepository
{
    public FurnitureGoodRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<IReadOnlyList<FurnitureGoodEntity>> QueryFurniture(GetFurnitureGoodsContainer paramsContainer,
        CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
SELECT * FROM furniture_goods
WHERE
    name ILIKE CASE
                WHEN (@FilterName = '') IS NOT FALSE THEN name
                ELSE '%' || @FilterName || '%'
           END 
    AND
    (price >= @MinPrice) AND
    (price <= @MaxPrice) AND
    (release_date >= @MinReleaseDate) AND
    (release_date <= @MaxReleaseDate);
";

        await using NpgsqlConnection connection = await GetAndOpenConnection(cancellationToken);


        var sqlQueryParams = new
        {
            FilterName = paramsContainer.Name,
            MinPrice = paramsContainer.PriceMinRange,
            MaxPrice = paramsContainer.PriceMaxRange == 0 ? decimal.MaxValue : paramsContainer.PriceMaxRange,
            MinReleaseDate = paramsContainer.ReleaseDateMinRange,
            MaxReleaseDate = paramsContainer.ReleaseDateMaxRange == DateOnly.MinValue
                ? DateOnly.MaxValue
                : paramsContainer.ReleaseDateMaxRange
        };

        var furnitureGoods = await connection.QueryAsync<FurnitureGoodEntity>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );

        return furnitureGoods.ToList();
    }

    public async Task AddFurniture(FurnitureGoodEntity[] goods, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
INSERT INTO furniture_goods (name, price, release_date)
    SELECT name, price, release_date
    FROM UNNEST(@Goods)
    returning id;
";

        var sqlParameters = new
        {
            Goods = goods
        };

        await using NpgsqlConnection connection = await GetAndOpenConnection(cancellationToken);

        var fakeFurnitureIds = await connection.QueryAsync<long>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlParameters,
                cancellationToken: cancellationToken
            )
        );
    }
}