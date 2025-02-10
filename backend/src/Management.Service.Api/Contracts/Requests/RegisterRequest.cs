namespace Management.Service.Api.Contracts.Requests;

public record RegisterRequest(
    string? Username,
    string? Email,
    string? Password
);