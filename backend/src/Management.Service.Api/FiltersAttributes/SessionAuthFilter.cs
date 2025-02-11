using Management.Service.Api.FiltersAttributes.Utils;
using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Domain.Contracts.Dal.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Management.Service.Api.FiltersAttributes;

public class SessionAuthFilter : IAsyncAuthorizationFilter
{
    private readonly ICredentialsRepository _credentialsRepository;
    private readonly ILogger<SessionAuthFilter> _logger;

    public SessionAuthFilter(ICredentialsRepository credentialsRepository, ILogger<SessionAuthFilter> logger)
    {
        _credentialsRepository = credentialsRepository;
        _logger = logger;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var cancellationToken = context.HttpContext.RequestAborted;

        string? sessionId = context.HttpContext.Request.Cookies["rf-session-id"];
        if (sessionId == null)
        {
            ErrorRequestHandler.HandleUnauthorizedRequest(context, "Credentials were not provided.");
            return;
        }

        IReadOnlyList<UserSessionEntity> sessionEntityList;
        try
        {
            sessionEntityList = await _credentialsRepository.GetSessionCredentials(
                sessionId: sessionId!,
                cancellationToken: cancellationToken
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{time} | Unexpected exception occured during session authorization.", DateTime.Now);
            ErrorRequestHandler.HandleInternalError(context);
            return;
        }

        if (sessionEntityList.Count == 0)
        {
            ErrorRequestHandler.HandleUnauthorizedRequest(context, "Invalid credentials.");
            return;
        }

        var sessionEntity = sessionEntityList[0];
        if (DateTimeOffset.UtcNow > sessionEntity.ExpirationDate)
        {
            ErrorRequestHandler.HandleUnauthorizedRequest(context, "Credentials expired.");
            return;
        }
    }
}