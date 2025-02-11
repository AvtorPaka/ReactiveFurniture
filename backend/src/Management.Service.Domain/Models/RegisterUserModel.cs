namespace Management.Service.Domain.Models;

public record RegisterUserModel(
    string Username,
    string Email,
    string Password
);