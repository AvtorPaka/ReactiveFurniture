using Management.Service.Domain.Contracts.Dal.Entities;

namespace Management.Service.Domain.Contracts.Dal.Interfaces;

public interface ICredentialsRepository: IDbRepository
{
    public Task AddUserCredentials(UserCredentialEntity entity, CancellationToken cancellationToken);
    public Task<UserCredentialEntity> GetUserByEmail(string userEmail, CancellationToken cancellationToken);
    public Task<UserCredentialEntity> GetUserBySessionId(string sessionId, CancellationToken cancellationToken);
    public Task<IReadOnlyList<UserSessionEntity>> GetSessionCredentials(string sessionId, CancellationToken cancellationToken);
    public Task CreateUserSession(UserSessionEntity entity, CancellationToken cancellationToken);
    public Task DeleteUserSession(string sessionId, CancellationToken cancellationToken);
}