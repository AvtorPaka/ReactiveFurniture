namespace Management.Service.Domain.Models;

public record LoginUserModel(
    string UserCred,
    string Password
);