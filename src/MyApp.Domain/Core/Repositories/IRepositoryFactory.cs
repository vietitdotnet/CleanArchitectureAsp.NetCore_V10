using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Core.Repositories
{
    public interface IRepositoryFactory
    {
        IBaseRepository<T, TKey> Create<T, TKey>() where T : BaseEntity<TKey> where TKey : notnull;
    }
}
