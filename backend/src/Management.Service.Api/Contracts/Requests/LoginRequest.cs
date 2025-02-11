namespace Management.Service.Api.Contracts.Requests;

public record LoginRequest(
    string? Email,
    string? Password
);