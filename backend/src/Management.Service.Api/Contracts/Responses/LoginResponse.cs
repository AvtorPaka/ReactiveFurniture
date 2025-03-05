namespace Management.Service.Api.Contracts.Responses;

public record LoginResponse(
    string Username,
    string Email
);