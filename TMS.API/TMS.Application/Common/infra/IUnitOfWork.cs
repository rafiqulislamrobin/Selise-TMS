using CSharpFunctionalExtensions;

namespace TMS.Application.Common.infra;

public interface IUnitOfWork
{
    IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Entity<TKey>;

    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
