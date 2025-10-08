using GLP_DocumentaryProcess.Infrastructure.Context;

namespace GLP_DocumentaryProcess.Infrastructure.Persistence.UnitOfWork;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly DocumentaryProcessDbContext _db;
    public EfUnitOfWork(DocumentaryProcessDbContext db) => _db = db;
    public Task<int> SaveChanges(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
}