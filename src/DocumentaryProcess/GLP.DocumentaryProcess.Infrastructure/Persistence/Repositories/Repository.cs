using System.Linq.Expressions;
using GLP_DocumentaryProcess.Infrastructure.Context;
using GLP.DocumentaryProcess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GLP_DocumentaryProcess.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DocumentaryProcessDbContext _db;
    protected readonly DbSet<T> _set;

    public Repository(DocumentaryProcessDbContext db)
    {
        _db = db;
        _set = db.Set<T>();
    }
    
    public async Task<T?> GetById(Guid id, CancellationToken ct = default)
        => await _set.FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task<IReadOnlyList<T>> List(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? include = null, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task Add(T entity, CancellationToken ct = default)
        => await _set.AddAsync(entity, ct);

    public async Task AddRange(IEnumerable<T> entities, CancellationToken ct = default)
        => await _set.AddRangeAsync(entities, ct);

    public void Update(T entity) => _set.Update(entity);

    public Task SoftDelete(Guid id, string? deletedBy = null, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}