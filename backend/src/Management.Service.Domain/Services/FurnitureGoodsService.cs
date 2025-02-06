using FluentValidation;
using Management.Service.Domain.Contracts.Dal.Interfaces;
using Management.Service.Domain.Exceptions;
using Management.Service.Domain.Mappers;
using Management.Service.Domain.Models;
using Management.Service.Domain.Services.Interfaces;
using Management.Service.Domain.Validators;
using Microsoft.Extensions.Logging;

namespace Management.Service.Domain.Services;

public class FurnitureGoodsService : IFurnitureGoodsService
{
    private readonly IFurnitureGoodRepository _furnitureRepository;
    private readonly ILogger<FurnitureGoodsService> _logger;

    public FurnitureGoodsService(IFurnitureGoodRepository repository, ILogger<FurnitureGoodsService> logger)
    {
        _furnitureRepository = repository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<FurnitureGoodModel>> GetFurniture(GetFurnitureGoodModel model, CancellationToken cancellationToken)
    {
        try
        {
            return await GetFurnitureUnsafe(
                model,
                cancellationToken
            );
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "{time} | Invalid request parameters during GetFurnitureGoods call.", DateTime.Now);
            throw new DomainException("Invalid request parameters.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{time} | Unexpected exception occured during GetFurnitureGoods call.", DateTime.Now);
            throw;
        }
    }

    private async Task<IReadOnlyList<FurnitureGoodModel>> GetFurnitureUnsafe(GetFurnitureGoodModel model,
        CancellationToken cancellationToken)
    {
        var validator = new GetFurnitureModelValidator();
        await validator.ValidateAndThrowAsync(model, cancellationToken);

        var entities = await _furnitureRepository.QueryFurniture(
            paramsContainer: model.MapModelToContainer(),
            cancellationToken: cancellationToken
        );

        return entities.MapEntitiesToModels();
    }
}