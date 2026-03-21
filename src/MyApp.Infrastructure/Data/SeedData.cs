using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Exceptions;
using MyApp.Infrastructure.Models;
using MyApp.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyApp.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            const string adminUserName = "Admin";
            const string adminPhone = "0355445775";
            const string adminEmail = "viet99cm@gmail.com";
            const string adminPassword = "Viet123456@";

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            string admintratorRole = AuthRoler.Administrator.ToString();
            string adminRole = AuthRoler.Admin.ToString();
            string employeeRole = AuthRoler.Employee.ToString();

            // Tạo role nếu chưa có
            if (!await roleManager.RoleExistsAsync(admintratorRole))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(admintratorRole));
                if (!result.Succeeded)
                    throw new BadRequestException($"Cannot create role {admintratorRole}");
            }

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(adminRole));
                if (!result.Succeeded)
                    throw new BadRequestException($"Cannot create role {adminRole}");
            }

            if (!await roleManager.RoleExistsAsync(employeeRole))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(employeeRole));
                if (!result.Succeeded)
                    throw new BadRequestException($"Cannot create role {employeeRole}");
            }

            // Tạo user admin nếu chưa có
            var adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    PhoneNumber = adminPhone,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!createResult.Succeeded)
                    throw new BadRequestException($"Cannot create user admin {adminUser}");

                // Gán role Admin
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
    }
}
