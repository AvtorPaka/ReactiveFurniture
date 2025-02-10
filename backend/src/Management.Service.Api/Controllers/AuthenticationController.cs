using Management.Service.Api.Contracts.Requests;
using Management.Service.Api.Contracts.Responses;
using Management.Service.Api.Mappers;
using Management.Service.Domain.Models;
using Management.Service.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Management.Service.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserCredentialsService _credentialsService;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IUserCredentialsService credentialsService,
        ILogger<AuthenticationController> logger)
    {
        _credentialsService = credentialsService;
        _logger = logger;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType<RegisterResponse>(200)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        await _credentialsService.RegisterNewUser(
            registerModel: request.MapRequestToModel(),
            cancellationToken: cancellationToken
        );
        
        return Ok(new RegisterResponse());
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType<LoginResponse>(200)]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var setCookieModel = await _credentialsService.LoginUser(
            loginModel: request.MapRequestToModel(),
            cancellationToken: cancellationToken
        );
        
        Response.Cookies.Append("rf-session-id", setCookieModel.SessionId, new CookieOptions
        {
            Expires = setCookieModel.ExpirationDate,
            IsEssential = true,
            Domain = Request.Host.Host
        });

        return Ok(new LoginResponse());
    }

    // Authorize 
    [HttpPost]
    [Route("logout")]
    [ProducesResponseType<LogoutResponse>(200)]
    public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
    {
        await _credentialsService.LogoutUser(
            logoutModel: new LogoutUserModel(
                SessionId: Request.Cookies["rf-session-id"]!
            ),
            cancellationToken: cancellationToken
        );
        
        Response.Cookies.Delete("rf-session-id", new CookieOptions
        {
            IsEssential = true,
            Domain = Request.Host.Host
        });

        return Ok(new LogoutResponse());
    }
}