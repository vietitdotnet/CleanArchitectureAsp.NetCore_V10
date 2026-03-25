using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Infrastructure.Data;
using System;

namespace MyApp.Infrastructure.Services.Common
{
    public abstract class BaseService
    {
        protected readonly MyAppDbContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _serviceProvider;

        protected BaseService(
            MyAppDbContext context,
            IMapper mapper,
            IServiceProvider serviceProvider)
        {
            _dbContext = context;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        protected async Task ValidateAsync<T>(
            T request,
            CancellationToken ct = default)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();

            if (validator != null)
                await validator.ValidateAndThrowAsync(request, ct);
        }
    }
}
