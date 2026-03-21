using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Repositories;
using MyApp.Infrastructure.Data;
using NLog.Config;

namespace MyApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MyAppDbContext _dbContext;
        private readonly IRepositoryFactory _factory;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(IRepositoryFactory factory , MyAppDbContext dbContext)
        {
             _factory = factory;
             _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();

        }

        public IBaseRepository<T, TKey> Repository<T, TKey>()
         where T : BaseEntity<TKey>
         where TKey : notnull
        {
            var entityType = typeof(T);

            if (!_repositories.TryGetValue(entityType, out var repo))
            {
                repo = _factory.Create<T, TKey>();
                _repositories[entityType] = repo;
            }

            return (IBaseRepository<T, TKey>)repo;
        }


        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _dbContext.SaveChangesAsync(ct);
        }

        public async Task RollBackChangesAsync(CancellationToken ct = default)
        {
            await _dbContext.Database.RollbackTransactionAsync(ct);
        }

       
    }
}