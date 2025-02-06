using FluentValidation;
using Management.Service.Domain.Contracts.Dal.Interfaces;
using Management.Service.Domain.Mappers;
using Management.Service.Domain.Models;
using Management.Service.Domain.Services.Interfaces;
using Management.Service.Domain.Validators;

namespace Management.Service.Domain.Services;

public class FurnitureGoodsService : IFurnitureGoodsService
{
    private readonly IFurnitureGoodRepository _furnitureRepository;

    public FurnitureGoodsService(IFurnitureGoodRepository repository)
    {
        _furnitureRepository = repository;
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
            Console.WriteLine(ex);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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