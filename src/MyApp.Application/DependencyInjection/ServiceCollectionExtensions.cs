using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Administrative;
using MyApp.Application.Features.Categorys;
using MyApp.Application.Features.Orders;
using MyApp.Application.Features.Products;
using MyApp.Application.Features.ProductUints;
using MyApp.Application.Features.PromotionItems;
using MyApp.Application.Features.Promotions;
using MyApp.Application.Interfaces.Common;

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
            
            services.AddTransient<ISlugService, SlugService>();

            services.AddScoped<ICategoryService,  CategoryService>();

            services.AddScoped<IPromotionService, PromotionService>();

            services.AddScoped<IAdministrativeServcie, AdministrativeServcie>();

            services.AddScoped<IProductUnitService, ProductUnitService>();

            services.AddScoped<IPromotionItemService, PromotionItemService>();

            return services;
        }
    }
}
