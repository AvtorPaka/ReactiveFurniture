namespace Management.Service.Api.Contracts.Responses;

public record CheckAuthResponse(
    string Username,
    string Email
);