using System.Transactions;
using Management.Service.Domain.Contracts.Dal.Interfaces;
using Npgsql;

namespace Management.Service.Infrastructure.Dal.Repositories;

public abstract class BaseRepository: IDbRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public BaseRepository(NpgsqlDataSource npgsqlDataSource)
    {
        _dataSource = npgsqlDataSource;
    }

    protected async Task<NpgsqlConnection> GetAndOpenConnection(CancellationToken cancellationToken)
    {
        var connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        await connection.ReloadTypesAsync(cancellationToken);

        return connection;
    }
    
    public TransactionScope CreateTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            scopeOption: TransactionScopeOption.Required,
            transactionOptions: new TransactionOptions
            {
                IsolationLevel = isolationLevel,
                Timeout = TimeSpan.FromSeconds(5)
            },
            asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled
        );
    }
}