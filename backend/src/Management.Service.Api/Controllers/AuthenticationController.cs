using Management.Service.Api.Contracts.Requests;
using Management.Service.Api.Contracts.Responses;
using Management.Service.Api.FiltersAttributes;
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

    public AuthenticationController(IUserCredentialsService credentialsService)
    {
        _credentialsService = credentialsService;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType<RegisterResponse>(200)]
    [ProducesResponseType<ErrorResponse>(409)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        await _credentialsService.RegisterNewUser(
            registerModel: request.MapRequestToModel(),
            cancellationToken: cancellationToken
        );

        return Ok(new RegisterResponse());
    }

    [HttpGet]
    [Route("check-auth")]
    [ProducesResponseType<CheckAuthResponse>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    public async Task<IActionResult> CheckAuthentication(CancellationToken cancellationToken)
    {
        SetCookieModel model = await _credentialsService.CheckUserAuth(
            sessionId: HttpContext.Request.Cookies["rf-session-id"],
            cancellationToken: cancellationToken
        );

        return Ok(new CheckAuthResponse(
            Username: model.Username,
            Email: model.Email
        ));
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

        return Ok(new LoginResponse(
            Email: setCookieModel.Email,
            Username: setCookieModel.Username
        ));
    }

    [HttpPost]
    [Route("logout")]
    [ServiceFilter(typeof(SessionAuthFilter))]
    [ProducesResponseType<LogoutResponse>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
    {
        await _credentialsService.LogoutUser(
            logoutModel: new LogoutUserModel(
                SessionId: Request.Cookies["rf-session-id"] ?? ""
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