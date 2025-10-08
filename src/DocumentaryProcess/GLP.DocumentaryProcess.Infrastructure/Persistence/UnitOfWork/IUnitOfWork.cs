namespace GLP_DocumentaryProcess.Infrastructure.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChanges(CancellationToken ct = default);
}