using System.Net;
using Management.Service.Api.Contracts.Requests;
using Management.Service.Api.Contracts.Responses;
using Management.Service.Api.Mappers;
using Management.Service.Domain.Models;
using Management.Service.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Management.Service.Api.Controllers;

[ApiController]
[Route("goods")]
public class FurnitureGoodsController : ControllerBase
{
    private readonly IFurnitureGoodsService _furnitureGoodsService;
    private readonly ILogger<FurnitureGoodsController> _logger;

    public FurnitureGoodsController(IFurnitureGoodsService furnitureGoodsService,
        ILogger<FurnitureGoodsController> logger)
    {
        _furnitureGoodsService = furnitureGoodsService;
        _logger = logger;
    }

    [HttpGet]
    [Route("get-furniture")]
    [ProducesResponseType<IEnumerable<GetFurnitureGoodsResponse>>(200)]
    public async Task<IActionResult> GetFurnitureGoods([FromQuery] GetFurnitureGoodsRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("{time} | Started executing goods/get-furniture method", DateTime.Now);
        IReadOnlyList<FurnitureGoodModel> furnitureModels = await _furnitureGoodsService.GetFurniture(
            model: request.MapRequestToModel(),
            cancellationToken: cancellationToken
        );

        _logger.LogInformation("{time} | Ended executing goods/get-furniture method", DateTime.Now);
        if (furnitureModels.Count == 0)
        {
            return NotFound(new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Exceptions: ["Entities not found."]
            ));
        }

        IReadOnlyList<GetFurnitureGoodsResponse> response = furnitureModels.MapModelsToResponse();
        return Ok(response);
    }
}