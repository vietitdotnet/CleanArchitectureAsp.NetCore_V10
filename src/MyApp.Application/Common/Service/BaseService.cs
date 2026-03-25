using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Common.Service
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _serviceProvider;

        protected BaseService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
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
