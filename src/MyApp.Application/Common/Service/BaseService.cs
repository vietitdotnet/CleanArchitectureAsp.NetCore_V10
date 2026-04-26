using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Common.Results;
using MyApp.Application.Interfaces.Common;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
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

        protected async Task<OperationResult<T>> ValidateAsync<T>(
             T request,
             CancellationToken ct = default)
                {
                    var validator = _serviceProvider.GetService<IValidator<T>>();

                    if (validator == null)
                        return OperationResult<T>.Ok(default!);

                    var result = await validator.ValidateAsync(request, ct);

                    if (!result.IsValid)
                    {
                        var errors = result.Errors
                            .GroupBy(e => ToCamelCase(e.PropertyName))
                            .ToDictionary(
                                g => NormalizeKey(g.Key),
                                g => g.Select(x => x.ErrorMessage).ToArray()
                            );

                        return OperationResult<T>.Fail(errors);
                    }

            return OperationResult<T>.Ok(default!);
        }


        private static string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        private static string NormalizeKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return "general";

            key = key.Replace("$.", "");


            if (key.StartsWith("req.", StringComparison.OrdinalIgnoreCase))
                key = key.Substring(4);

            if (key.EndsWith(".Value"))
                key = key.Replace(".Value", "");

            return char.ToLowerInvariant(key[0]) + key.Substring(1);
        }

    }

}
