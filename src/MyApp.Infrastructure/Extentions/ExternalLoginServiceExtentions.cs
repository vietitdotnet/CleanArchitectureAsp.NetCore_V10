using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyApp.Infrastructure.Extentions
{
    public static class ExternalLoginServiceExtentions
    {
        public static IServiceCollection AddExternalLoginService(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication()
                 .AddGoogle(options =>
                 {
                     Console.WriteLine("Secret = " + config["JWT:Secret"]);
                     options.ClientId = config["Authentication:Google:ClientId"]!;
                     Console.WriteLine("Google ClientID = " + options.ClientId);
                     options.ClientSecret = config["Authentication:Google:ClientSecret"]!;
                     Console.WriteLine("Google ClientSecret = " + options.ClientSecret);
                     options.CallbackPath = "/signin-google";
                 })
                  .AddFacebook(options =>
                  {
                      Console.WriteLine("====== FACEBOOK CONFIG ======");
                      Console.WriteLine("Facebook AppId     = " + config["Authentication:Facebook:AppId"]);
                      Console.WriteLine("Facebook AppSecret = " + config["Authentication:Facebook:AppSecret"]);
                      Console.WriteLine("==============================");

                      options.AppId = config["Authentication:Facebook:AppId"]!;
                      options.AppSecret = config["Authentication:Facebook:AppSecret"]!;

                      // Callback phải trùng Facebook Developer console
                      options.CallbackPath = "/signin-facebook";
                      options.Scope.Add("email");
                  });

            return services;
        }
    }
}
