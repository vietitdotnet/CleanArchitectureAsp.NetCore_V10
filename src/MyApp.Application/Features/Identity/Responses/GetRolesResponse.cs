using MyApp.Application.Features.Identity.DTOs;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Identity.Responses
{
    public  class GetRolesResponse
    {
        public PagedResponse<RoleDto, RoleParameters> Data { get; private set; }
        public GetRolesResponse(PagedResponse<RoleDto, RoleParameters> roles)
        {
            Data = roles;
        }
    }
}
