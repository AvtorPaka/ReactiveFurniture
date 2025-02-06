using System.Transactions;
using Management.Service.Domain.Contracts.Dal.Interfaces;

namespace Management.Service.Infrastructure.Dal.Repositories;

public abstract class BaseRepository: IDbRepository
{
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