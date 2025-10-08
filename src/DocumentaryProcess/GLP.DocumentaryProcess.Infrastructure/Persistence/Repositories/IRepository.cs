using System.Linq.Expressions;
using GLP.DocumentaryProcess.Domain.Entities;

namespace GLP_DocumentaryProcess.Infrastructure.Persistence.Repositories;

public interface IRepository<T> where T: BaseEntity
{
    Task<T> GetById(Guid id, CancellationToken ct = default);
    
    Task<IReadOnlyList<T>> List(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? include = null,                               
        CancellationToken ct = default);
    
    Task Add(T entity, CancellationToken ct = default);
    Task AddRange(IEnumerable<T> entities, CancellationToken ct = default);
    void Update(T entity);
    Task SoftDelete(Guid id, string? deletedBy = null, CancellationToken ct = default);
}