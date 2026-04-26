
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Common.Results;
using MyApp.Application.Features.Identity.DTOs;
using MyApp.Application.Features.Identity.Requests;
using MyApp.Application.Interfaces.Identity;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Infrastructure.Common.Queryable.Extentions;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Entities.Identity;
using MyApp.Infrastructure.Exceptions.Extention;
using MyApp.Infrastructure.Services.Common;
using System.Data;
namespace MyApp.Infrastructure.Services.Identity
{
    public class IdentityService : BaseService, IIDentityService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(MyAppDbContext context, 
            IMapper mapper, 
            IServiceProvider serviceProvider,
            RoleManager<IdentityRole> roleManager) : base(context, mapper, serviceProvider)
        {
            _roleManager = roleManager;
        }

        public async Task<PagedResponse<UserDto, UserParameters>> GetUsers(UserParameters parameters)
        {
            var user = _dbContext.AutUsers.AsNoTracking();

            var users = await user.ApplyPagingWithProjection<AppUser, UserDto, UserParameters>(parameters, _mapper.ConfigurationProvider);

            return users;

        }

        public async Task<OperationResult<string>> CreateRoleAsync(CreateRoleRequest request, CancellationToken ct = default)
        {
           

            await ValidateAsync(request);

            var role = new IdentityRole(request.Name);

            var result = await _roleManager.CreateAsync(role);

            result.ThrowIfFailed();

            return OperationResult<string>.Ok(role.Id, "Tạo role thành công");
                
        }

        public async Task<PagedResponse<RoleDto, RoleParameters>> GetRoles(RoleParameters parameters)
        {
            // Tạo query ban đầu
            var query = _dbContext.Roles.AsNoTracking();

            // Thực hiện Select ngay trong ApplyPaging để EF Core dịch thành SQL gom nhóm
            var pagedRoles = await query.ApplyPagingWithSelector(parameters, x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name!,
                // EF Core sẽ tự tối ưu truy vấn con này thành SQL JOIN/JSON_QUERY tùy phiên bản
                Claims = _dbContext.RoleClaims
                    .Where(rc => rc.RoleId == x.Id)
                    .Select(rc => rc.ClaimType + "=" + rc.ClaimValue)
                    .ToArray()
            });

            return pagedRoles;
        }
    }
}
