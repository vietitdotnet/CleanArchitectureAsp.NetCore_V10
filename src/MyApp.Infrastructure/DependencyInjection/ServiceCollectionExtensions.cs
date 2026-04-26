using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.Auth;
using MyApp.Application.Interfaces.Common;
using MyApp.Application.Interfaces.External;
using MyApp.Application.Interfaces.Identity;
using MyApp.Domain.Core.Repositories;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Entities.Identity;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.Services.Auth;
using MyApp.Infrastructure.Services.Common;
using MyApp.Infrastructure.Services.External;
using MyApp.Infrastructure.Services.Identity;

namespace MyApp.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MyAppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SQLServerAppDatabase"),
                    x => x.MigrationsAssembly(typeof(MyAppDbContext).Assembly.FullName)
                ));


            services.AddScoped<IRepositoryFactory, RepositoryFactory>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
   

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true; // Yêu cầu xác nhận tài khoản trước khi đăng nhập
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 8; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;
                

            })
            .AddEntityFrameworkStores<MyAppDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IIDentityService, IdentityService>();
            services.AddSingleton<IHashService, HashService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IRequestInfoService, RequestInfoService>();

            services.AddSingleton<IEmailQueue, EmailQueue>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<EmailSender>();
            services.AddHostedService<EmailBackgroundWorker>();



            return services;
        }

        public static void MigrateDatabase(this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<MyAppDbContext>();

            dbContext.Database.Migrate();
        }
    }


}
