using Labb4_MVC_Razer.Data;
using Labb4_MVC_Razor.Data;
using Labb4_MVC_Razor.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Labb4_MVC_Razor.Utility
{
    public static class DbInitializer
    {
        private static readonly Random _random = new();
        private const string DefaultPassword = "Password123!";

        public static async Task Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // Check if the database has been seeded
            if (context.Customers.Any())
            {
                return; // DB has been seeded
            }

            // Create roles
            string[] roleNames = { "Customer", "Employee" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Customers
            var customers = new[]
            {
                new IdentityUser { UserName = "emma_andersson92@gmail.com", Email = "emma_andersson92@gmail.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber()},
                new IdentityUser { UserName = "johan.lindberg_85@yahoo.com", Email = "johan.lindberg_85@yahoo.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber()},
                new IdentityUser { UserName = "anna.karlsson@telia.com", Email = "anna.karlsson@telia.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber() },
                new IdentityUser { UserName = "erik.nilsson77@hotmail.com", Email = "erik.nilsson77@hotmail.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber() },
                new IdentityUser { UserName = "maria.gustafsson_88@outlook.com", Email = "maria.gustafsson_88@outlook.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber() },
                new IdentityUser { UserName = "amanda_eriksson@telia.com", Email = "amanda_eriksson@telia.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber() },
                new IdentityUser { UserName = "sara.persson90@gmail.com", Email = "sara.persson90@gmail.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber() },
                new IdentityUser { UserName = "mattias_berg_79@hotmail.com", Email = "mattias_berg_79@hotmail.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber() },
                new IdentityUser { UserName = "linnea_larsson85@telia.com", Email = "linnea_larsson85@telia.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber() },
                new IdentityUser { UserName = "daniel.svensson1980@yahoo.com", Email = "daniel.svensson1980@yahoo.com", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber()}
            };

            foreach (var customer in customers)
            {
                await CreateUser(userManager, customer, "Customer", context);
            }

            // Employees
            var employees = new[]
            {
                new IdentityUser { UserName = "marianne.gustafsson@bibliotek.edu.se", Email = "marianne.gustafsson@bibliotek.edu.se", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber()},
                new IdentityUser { UserName = "anna.eriksson@bibliotek.edu.se", Email = "anna.eriksson@bibliotek.edu.se", EmailConfirmed = true, PhoneNumber = GenerateRandomPhoneNumber() }
            };

            foreach (var employee in employees)
            {
                await CreateUser(userManager, employee, "Employee", context);
            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateUser(UserManager<IdentityUser> userManager, IdentityUser user, string role, ApplicationDbContext context)
        {
            var result = await userManager.CreateAsync(user, DefaultPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                var customerEntity = new Customer
                {
                    ApplicationUserId = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                context.Customers.Add(customerEntity);
            }
            else
            {
                // Handle the errors if needed (e.g., log them)
                // Example: Log.Error(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        private static string GenerateRandomPhoneNumber()
        {
            return $"07{_random.Next(0, 10000000):0000000}";
        }
    }
}