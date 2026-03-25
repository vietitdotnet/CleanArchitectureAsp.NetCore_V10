using Microsoft.AspNetCore.Identity.UI.Services;
using MyApp.Application.DependencyInjection;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.DependencyInjection;
using MyApp.Infrastructure.Models;
using MyApp.WebMvc.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().

AddRazorPagesOptions(options => {
    options.Conventions.AddAreaPageRoute(
        areaName: "Identity",
        pageName: "/Account/Login",
        route: "dang-nhap"
        );

    options.Conventions.AddAreaPageRoute(
     areaName: "Identity",
     pageName: "/Account/Register",
     route: "dang-ky"
     );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();


app.Run();
