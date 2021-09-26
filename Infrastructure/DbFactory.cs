using System;

namespace Infrastructure
{
    public sealed class DbFactory : IDisposable
    {
        private bool _disposed;
        private readonly Func<AppDbContext> _instanceFunc;
        private AppDbContext _dbContext;
        public AppDbContext DbContext => _dbContext ??= _instanceFunc.Invoke();

        public DbFactory(Func<AppDbContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}