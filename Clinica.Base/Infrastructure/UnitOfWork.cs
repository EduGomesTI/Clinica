using Clinica.Base.Application;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Base.Infrastructure
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public UnitOfWork(TDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _semaphore.WaitAsync(cancellationToken);
                try
                {
                    return await _context.SaveChangesAsync(cancellationToken);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle database-specific errors here
                throw new InvalidOperationException("Database update failed", ex);
            }
            catch (Exception ex)
            {
                // Handle other errors here
                throw new InvalidOperationException("An error occurred while saving changes", ex);
            }
        }
    }
}