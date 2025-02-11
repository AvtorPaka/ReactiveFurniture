using System.Net;
using Management.Service.Api.Contracts.Requests;
using Management.Service.Api.Contracts.Responses;
using Management.Service.Api.FiltersAttributes;
using Management.Service.Api.Mappers;
using Management.Service.Domain.Models;
using Management.Service.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Management.Service.Api.Controllers;

[ApiController]
[Route("goods")]
[ServiceFilter(typeof(SessionAuthFilter))]
public class FurnitureGoodsController : ControllerBase
{
    private readonly IFurnitureGoodsService _furnitureGoodsService;

    public FurnitureGoodsController(IFurnitureGoodsService furnitureGoodsService)
    {
        _furnitureGoodsService = furnitureGoodsService;
    }

    [HttpGet]
    [Route("get-furniture")]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<IEnumerable<GetFurnitureGoodsResponse>>(200)]
    public async Task<IActionResult> GetFurnitureGoods([FromQuery] GetFurnitureGoodsRequest request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<FurnitureGoodModel> furnitureModels = await _furnitureGoodsService.GetFurniture(
            model: request.MapRequestToModel(),
            cancellationToken: cancellationToken
        );

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