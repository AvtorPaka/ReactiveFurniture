using Management.Service.Api.FiltersAttributes.Utils;
using Management.Service.Domain.Exceptions;
using Management.Service.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Management.Service.Api.FiltersAttributes;

public class SessionAuthFilter : IAsyncAuthorizationFilter
{
    private readonly IUserCredentialsService _credentialsService;

    public SessionAuthFilter(IUserCredentialsService credentialsService)
    {
        _credentialsService = credentialsService;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var cancellationToken = context.HttpContext.RequestAborted;

        try
        {
            var setCookieModel = await _credentialsService.CheckUserAuth(
                sessionId: context.HttpContext.Request.Cookies["rf-session-id"],
                cancellationToken: cancellationToken
            );
        }
        catch (UserUnauthenticatedException ex)
        {
            ErrorRequestHandler.HandleUnauthorizedRequest(context, ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            ErrorRequestHandler.HandleNotFoundRequest(context, ex);
        }
        catch (Exception)
        {
            ErrorRequestHandler.HandleInternalError(context);
        }
    }
}