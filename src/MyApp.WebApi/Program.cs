using Microsoft.OpenApi;
using MyApp.Application.DependencyInjection;
using MyApp.Infrastructure.DependencyInjection;
using MyApp.Infrastructure.Extentions;
using MyApp.WebApi.Exceptions.Extentions;
using MyApp.WebApi.Extentions;



var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddAuthenticationService(builder.Configuration)
    .AddApiServices()
    .AddUnifiedExceptionHandling();

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}


app.UseHttpsRedirection();

/*await app.Services.CreateAdmin();*/

/*app.HardCodedTokenMiddlewareExtention();*/

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();


app.Run();