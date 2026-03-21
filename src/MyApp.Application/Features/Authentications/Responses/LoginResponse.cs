using MyApp.Application.Features.Authentications.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Authentications.Responses
{
    public class LoginResponse
    {
        public bool Success { get;  set; }
        public string? Message { get;  set; }
        public AuthUserDto Data { get;  set; } = null!;

        public LoginResponse(bool success, string? message, AuthUserDto data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public LoginResponse()
        {

        }
    }
}
