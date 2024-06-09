using Microsoft.EntityFrameworkCore;

namespace Clinica.Base.Application
{
#pragma warning disable S2326

    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

#pragma warning restore S2326
}