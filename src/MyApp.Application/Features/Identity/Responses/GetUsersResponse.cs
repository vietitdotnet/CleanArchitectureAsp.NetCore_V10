using MyApp.Application.Features.Identity.DTOs;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MyApp.Application.Features.Identity.Responses
{
    public class GetUsersResponse
    {
        public PagedResponse<UserDto, UserParameters> Data { get; private set; }
         public GetUsersResponse(PagedResponse<UserDto, UserParameters> users)
        {
            Data = users;
        }
    }
}
