using MyApp.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Identity.Responses
{
    public class CreateRoleResponse
    {
        public OperationResult<string > Data { get; private set; } = null!;

        public CreateRoleResponse(OperationResult<string> data) { Data = data; }
    }
}
