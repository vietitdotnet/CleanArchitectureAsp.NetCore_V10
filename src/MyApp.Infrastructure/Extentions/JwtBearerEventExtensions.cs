using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyApp.Domain.Exceptions;

namespace MyApp.Infrastructure.Extentions
{
    public static class JwtBearerEventExtensions
    {
        public static void  AddCustomJwtEvents(this JwtBearerOptions options)
        {
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                   
                    return Task.CompletedTask;
                },

                OnChallenge = context =>
                {
                    context.HandleResponse(); // bỏ response mặc định

                    throw new UnauthorizedAccessException("Yêu cầu cần xác thực hợp lệ để truy cập.");

                },
                OnForbidden = context =>
                {
                    throw new ForbiddenAccessException("Bạn không có quyền truy cập tài nguyên này.");
                }
            };
        }

    }

}
