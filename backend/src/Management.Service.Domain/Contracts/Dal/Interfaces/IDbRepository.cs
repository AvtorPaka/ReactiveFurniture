using System.Transactions;

namespace Management.Service.Domain.Contracts.Dal.Interfaces;

public interface IDbRepository
{
    public TransactionScope CreateTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}