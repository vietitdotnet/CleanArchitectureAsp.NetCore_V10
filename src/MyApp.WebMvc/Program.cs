using Microsoft.AspNetCore.Identity.UI.Services;
using MyApp.Application.DependencyInjection;
using MyApp.Infrastructure.DependencyInjection;
using MyApp.WebMvc.Services;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IEmailSender, EmailSender>();
// Add services to the container.

builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AppExceptionFilter>();
    options.Filters.Add<NormalizeFilter>();
})
.AddMvcOptions(options =>
{
    // 1. Việt hóa lỗi khi nhập chữ vào ô số (int, decimal, double...)
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
        (fieldName) => $"Trường {fieldName} phải là chữ số.");

    // 2. Việt hóa lỗi khi để trống trường bắt buộc (kiểu Value Type không nullable)
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        (fieldName) => $"{fieldName} không được để trống.");

    // 3. Việt hóa lỗi khi giá trị nhập vào không khớp kiểu dữ liệu
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
        (value, fieldName) => $"Giá trị '{value}' không hợp lệ cho trường {fieldName}.");

    // 4. Việt hóa lỗi cho kiểu DateTime hoặc các kiểu phức tạp khác
    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
        (fieldName) => $"Giá trị của {fieldName} không hợp lệ.");

    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});



builder.Services.AddRazorPages().AddRazorPagesOptions
    (options => {
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
app.UseStaticFiles();

app.MapAreaControllerRoute(
            name: "Manager",
            pattern: "Manager/{controller}/{action}/{id?}",
            areaName: "Manager",
            defaults: new
            {
                controller = "Product",
                action = "index"
            }
        );
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();



app.Run();
