namespace Management.Service.Domain.Models;

public record SetCookieModel(
    string SessionId,
    DateTimeOffset ExpirationDate
);