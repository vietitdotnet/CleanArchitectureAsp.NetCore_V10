using MyApp.Application.Common.Results;
using MyApp.Application.Features.Identity.DTOs;
using MyApp.Application.Features.Identity.Requests;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces.Identity
{
    public interface IIDentityService
    {
        Task<PagedResponse<UserDto, UserParameters>> GetUsers(UserParameters parameters);

        Task<OperationResult<string>> CreateRoleAsync(CreateRoleRequest request , CancellationToken ct = default);

        Task<PagedResponse<RoleDto, RoleParameters>> GetRoles(RoleParameters parameters);
    }
}
