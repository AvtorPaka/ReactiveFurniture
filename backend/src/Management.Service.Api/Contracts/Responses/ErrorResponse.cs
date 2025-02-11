using System.Net;

namespace Management.Service.Api.Contracts.Responses;

public record ErrorResponse(
    HttpStatusCode StatusCode,
    IEnumerable<string> Exceptions
);