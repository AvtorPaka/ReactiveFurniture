namespace Management.Service.Domain.Contracts.Dal.Entities;

public class UserCredentialEntity
{
    public long Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}