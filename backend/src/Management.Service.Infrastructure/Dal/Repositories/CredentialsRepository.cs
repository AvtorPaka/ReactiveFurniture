using Dapper;
using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Domain.Contracts.Dal.Interfaces;
using Management.Service.Domain.Exceptions;
using Npgsql;

namespace Management.Service.Infrastructure.Dal.Repositories;

public class CredentialsRepository : BaseRepository, ICredentialsRepository
{
    public CredentialsRepository(NpgsqlDataSource npgsqlDataSource) : base(npgsqlDataSource)
    {
    }

    public async Task AddUserCredentials(UserCredentialEntity entity, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
INSERT INTO user_credentials (username, email, password)
    VALUES (@Username, @Email, @Password)
    returning id;
";
        await using NpgsqlConnection connection = await GetAndOpenConnection(cancellationToken);

        var sqlParameters = new
        {
            entity.Username,
            entity.Email,
            entity.Password
        };

        try
        {
            var ids = await connection.QueryAsync<long>(
                new CommandDefinition(
                    commandText: sqlQuery,
                    parameters: sqlParameters,
                    cancellationToken: cancellationToken
                )
            );
        }
        catch (NpgsqlException ex)
        {
            if (ex.SqlState == "23505")
            {
                throw new EntityAlreadyExistsException($"Entity already exists.");
            }

            throw;
        }
    }

    public async Task CreateUserSession(UserSessionEntity entity, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<UserCredentialEntity> GetUser(string userEmail, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
SELECT * FROM user_credentials WHERE email = @Email;
";
        await using NpgsqlConnection connection = await GetAndOpenConnection(cancellationToken);

        var sqlParameters = new
        {
            Email = userEmail
        };

        var userEntity = await connection.QueryAsync<UserCredentialEntity>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlParameters,
                cancellationToken: cancellationToken
            )
        );
        
        var entityList = userEntity.ToList();
        if (entityList.Count == 0)
        {
            throw new EntityNotFoundException("Entity could not be found.");
        }

        return entityList[0];
    }


    // TODO: Rework contract
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