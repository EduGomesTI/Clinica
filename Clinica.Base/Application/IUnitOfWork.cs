using Microsoft.EntityFrameworkCore;

namespace Clinica.Base.Application
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}