using CSharpFunctionalExtensions;
using System;

namespace Application.Common.infra;

public interface IUnitOfWork
{
    IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Entity<TKey>;

    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
