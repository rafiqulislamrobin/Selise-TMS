using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Common.infra;

public interface IRepository<T, in TKey> where T : Entity<TKey>
{
    IQueryable<T> Query();

    Task<T> GetByIdAsync(TKey id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task AddAsync(T entity);

    void AddRangeAsync(List<T> entities);

    void RemoveRange(List<T> entities);

    void Update(T entity);

    void UpdateRange(List<T> entities);

    void Delete(T entity);
}