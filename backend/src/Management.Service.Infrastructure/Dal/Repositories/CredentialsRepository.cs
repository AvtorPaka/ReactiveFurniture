using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Domain.Contracts.Dal.Interfaces;
using Npgsql;

namespace Management.Service.Infrastructure.Dal.Repositories;

public class CredentialsRepository: BaseRepository, ICredentialsRepository
{
    public CredentialsRepository(NpgsqlDataSource npgsqlDataSource) : base(npgsqlDataSource)
    {
    }

    public async Task AddUserCredentials(UserCredentialEntity entity, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task CreateUserSession(UserSessionEntity entity, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<UserCredentialEntity> GetUser(string userEmail, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }


    public async Task<bool> CheckForSessionCredentials(string sessionId, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task DeleteUserSession(string sessionId, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}