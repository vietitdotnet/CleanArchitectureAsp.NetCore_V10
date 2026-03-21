using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBaseRepository<TEntity, TKey> Create<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : notnull
        {
            return _serviceProvider
                .GetRequiredService<IBaseRepository<TEntity, TKey>>();
        }

      
    }
}
