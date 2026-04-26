using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
using MyApp.WebApi.Services;


namespace MyApp.WebApi.Extentions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<NormalizeFilter>();
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }

}
