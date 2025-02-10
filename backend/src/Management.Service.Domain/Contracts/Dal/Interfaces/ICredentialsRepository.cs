using Management.Service.Domain.Contracts.Dal.Entities;

namespace Management.Service.Domain.Contracts.Dal.Interfaces;

public interface ICredentialsRepository: IDbRepository
{
    public Task AddUserCredentials(UserCredentialEntity entity, CancellationToken cancellationToken);
    public Task CreateUserSession(UserSessionEntity entity, CancellationToken cancellationToken);
    
    public Task<UserCredentialEntity> GetUser(string userEmail, CancellationToken cancellationToken);
    public Task<bool> CheckForSessionCredentials(string sessionId, CancellationToken cancellationToken);
    public Task DeleteUserSession(string sessionId, CancellationToken cancellationToken);
}