using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Features.Orders;
using MyApp.Application.Features.Products;

namespace MyApp.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services)
        {
            var assembly = (typeof(ApplicationAssemblyMarker)).Assembly;


            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(assembly);
            });

            services.AddValidatorsFromAssembly(assembly);
           

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IOderService, OrderService>();

            return services;
        }
    }
}
