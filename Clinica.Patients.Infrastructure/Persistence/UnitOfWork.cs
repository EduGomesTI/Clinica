using Clinica.Patients.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Patients.Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PatientDbContext _context;
        private bool _disposed;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public UnitOfWork(PatientDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _semaphore.WaitAsync();
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
                throw new Exception("Database update failed", ex);
            }
            catch (Exception ex)
            {
                // Handle other errors here
                throw new Exception("An error occurred while saving changes", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _semaphore.Wait();
                    try
                    {
                        _context.Dispose();
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }

                _disposed = true;
            }
        }
    }
}