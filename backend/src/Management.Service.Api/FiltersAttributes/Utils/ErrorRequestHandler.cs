using System.Net;
using Management.Service.Api.Contracts.Responses;
using Management.Service.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Management.Service.Api.FiltersAttributes.Utils;

internal static class ErrorRequestHandler
{
    internal static void HandleUnauthorizedRequest(AuthorizationFilterContext context, string invalidScenario)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.Unauthorized,
                Exceptions: ["Invalid credentials.", invalidScenario]
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.Unauthorized
        };

        context.Result = result;
    }
    
    internal static void HandleUnauthorizedRequest(ExceptionContext context, UserUnauthenticatedException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.Unauthorized,
                Exceptions: ["Invalid credentials.", ex.Message]
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.Unauthorized
        };

        context.Result = result;
    }
    
    internal static void HandleNotFoundRequest(ExceptionContext context, EntityNotFoundException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Exceptions: QueryExceptionMessage(ex)
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = result;
    }
    
    internal static void HandleNotFoundRequest(AuthorizationFilterContext context, EntityNotFoundException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Exceptions: QueryExceptionMessage(ex)
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = result;
    }
    
    internal  static void HandleConflictRequest(ExceptionContext context, EntityAlreadyExistsException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.Conflict,
                Exceptions: QueryExceptionMessage(ex)
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.Conflict
        };

        context.Result = result;
    }

    internal  static void HandleBadRequest(ExceptionContext context, Exception exception)
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

    internal  static void HandleInternalError(ExceptionContext context)
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
    
    internal  static void HandleInternalError(AuthorizationFilterContext context)
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