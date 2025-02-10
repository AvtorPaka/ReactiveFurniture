namespace Management.Service.Api.Contracts.Requests;

public record LoginRequest(
    string? UserCred,
    string? Password
);