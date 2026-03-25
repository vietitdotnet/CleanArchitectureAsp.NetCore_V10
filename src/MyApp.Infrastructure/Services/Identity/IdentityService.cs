
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Common.Results;
using MyApp.Application.Features.Identity.DTOs;
using MyApp.Application.Features.Identity.Requests;
using MyApp.Application.Interfaces.Identity;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Infrastructure.Common.Queryable.Extentions;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Entities.Identity;
using MyApp.Infrastructure.Services.Common;
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

        public async Task<OperationResult<string>> CreateRoleAsync(CreateRoleRequest request, CancellationToken ct = default)
        {
            await ValidateAsync(request);

            var role = new IdentityRole(request.Name);

            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded
                ? OperationResult<string>.Ok(role.Id, "Tạo role thành công")
                : OperationResult<string>.Fail(result.Errors.Select(x => x.Description));
        }

       

        public async Task<PagedResponse<UserDto, UserParameters>> GetUsers(UserParameters parameters)
        {
            var user = _dbContext.AutUsers.AsNoTracking();

            var users = await user.ApplyPagingWithProjection<AppUser, UserDto, UserParameters>(parameters, _mapper.ConfigurationProvider);

            return users;

        }

        Task<PagedResponse<UserDto, UserParameters>> IIDentityService.GetUsers(UserParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
