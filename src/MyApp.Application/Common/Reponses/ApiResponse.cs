using MyApp.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Common.Reponses
{
    public class ApiResponse
    {
        public bool Success { get; private set; } 
        public string? Message { get; private set; }

        public ApiResponse(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
