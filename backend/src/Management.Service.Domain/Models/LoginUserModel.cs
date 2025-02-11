namespace Management.Service.Domain.Models;

public record LoginUserModel(
    string Email,
    string Password
);