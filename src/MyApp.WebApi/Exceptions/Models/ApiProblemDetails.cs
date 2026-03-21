using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace MyApp.WebApi.Exceptions.Models
{
    public class ApiProblemDetails : ProblemDetails
    {
        public bool Success { get; set; } = false;
        public string ErrorCode { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
        public Dictionary<string, string[]>? Errors { get; set; }
        public string TraceId { get; set; } = null!;
    }
}


