using System.Net;
using Management.Service.Api.Contracts.Responses;
using Management.Service.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Management.Service.Api.FiltersAttributes;

public sealed class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case DomainException ex:
                HandleBadRequest(context, ex);
                break;
            default:
                HandleInternalError(context);
                break;
        }
    }

    private static void HandleBadRequest(ExceptionContext context, DomainException exception)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.BadRequest,
                Exceptions: QueryExceptionMessage(exception)
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.Result = result;
    }

    private static void HandleInternalError(ExceptionContext context)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.InternalServerError,
                Exceptions: ["Working on this."]
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.Result = result;
    }

    private static IEnumerable<string> QueryExceptionMessage(Exception exception)
    {
        yield return exception.Message;

        Exception? innerException = exception.InnerException;
        while (innerException != null)
        {
            yield return innerException.Message;
            innerException = innerException.InnerException;
        }
    }
}