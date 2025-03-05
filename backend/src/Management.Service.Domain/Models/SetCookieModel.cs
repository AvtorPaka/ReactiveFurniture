namespace Management.Service.Domain.Models;

public record SetCookieModel(
    string Username,
    string Email,
    string SessionId,
    DateTimeOffset ExpirationDate
);