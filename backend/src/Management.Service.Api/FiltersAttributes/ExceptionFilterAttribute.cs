using Management.Service.Api.FiltersAttributes.Utils;
using Management.Service.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Management.Service.Api.FiltersAttributes;

public sealed class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case DomainException ex:
                ErrorRequestHandler.HandleBadRequest(context, ex);
                break;
            case UserUnauthenticatedException ex:
                ErrorRequestHandler.HandleUnauthorizedRequest(context, ex);
                break;
            case UserAlreadyExistsException ex:
                ErrorRequestHandler.HandleConflictRequest(context, ex);
                break;
            case UserNotFoundException ex:
                ErrorRequestHandler.HandleNotFoundRequest(context, ex);
                break;
            case IncorrectCredentialsException ex:
                ErrorRequestHandler.HandleBadRequest(context, ex);
                break;
            default:
                ErrorRequestHandler.HandleInternalError(context);
                break;
        }
    }
}