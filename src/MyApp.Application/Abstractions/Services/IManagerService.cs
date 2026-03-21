using MyApp.Application.Features.Authentications.DTOs;
using MyApp.Application.Features.Managers.DTOs;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Abstractions.Services
{
    public interface IManagerService
    {
        Task<PagedResponse<UserDto, UserParameters>> GetUsers(UserParameters parameters);
    }
}
