
using Application.Common.infra;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Entity<TKey>
    {
        _repositories ??= new Dictionary<Type, object>();
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity, TKey>(_context);
        return (IRepository<TEntity, TKey>)_repositories[type];
    }
}
