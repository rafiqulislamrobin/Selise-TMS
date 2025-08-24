using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TMS.Application.Entities;
using Npgsql; // Required for PostgresException
using Task = System.Threading.Tasks.Task;

namespace TMS.API.Helper
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                // 1. Seed Roles
                string[] roles = new[] { "Admin", "Manager", "Employee" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // 2. Seed Users
                await CreateUser(userManager, "admin@demo.com", "Admin123!", "Admin User", "Admin");
                await CreateUser(userManager, "manager@demo.com", "Manager123!", "Manager User", "Manager");
                await CreateUser(userManager, "employee@demo.com", "Employee123!", "Employee User", "Employee");
            }
            catch (PostgresException ex) when (ex.SqlState == "3D000")
            {
                // Database not created yet — safe to skip
                Console.WriteLine("⚠️ Skipping SeedData: Database does not exist yet.");
            }
            catch (Exception ex)
            {
                // Log other issues
                Console.WriteLine($"❌ Error during SeedData: {ex.Message}");
                throw;
            }
        }

        private static async Task CreateUser(
            UserManager<User> userManager,
            string email,
            string password,
            string fullName,
            string role)
        {
            var existingUser = await userManager.FindByNameAsync(email);
            if (existingUser != null)
            {
                var inRole = await userManager.IsInRoleAsync(existingUser, role);
                if (!inRole)
                {
                    await userManager.AddToRoleAsync(existingUser, role);
                }
                return;
            }

            var user = new User();
            user.Create(fullName, email, role, password);
            user.UserName = email;
            user.EmailConfirmed = true;

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception($"Failed to create user {email}: {string.Join(", ", result.Errors)}");
            }
        }
    }
}
