using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Abstractions.Services;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.Features.Authentications.DTOs;
using MyApp.Application.Features.Managers.DTOs;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Infrastructure.Common.Extentions;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Models;

namespace MyApp.Infrastructure.Services
{
    public class ManagerService : IManagerService
    {
        private readonly MyAppDbContext _dbContext;

        private readonly IMapper _mapper;

        public ManagerService(MyAppDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;

            _mapper = mapper;
        }

        public async Task<PagedResponse<UserDto, UserParameters>> GetUsers(UserParameters parameters)
        {
            var user = _dbContext.AutUsers.AsNoTracking();

            var users = await user.ApplyPagingWithProjection<AppUser, UserDto, UserParameters>(parameters, _mapper.ConfigurationProvider);

            return users;

        }

    }
}
