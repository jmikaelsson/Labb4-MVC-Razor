using Labb4_MVC_Razor.Data;
using Labb4_MVC_Razor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb4_MVC_Razor.Data
{
    public static class DbInitializer
    {
        private static readonly Random _random = new();

        public static async Task Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;   // Database has already been seeded
            }

            // Define roles
            string[] roleNames = { "Employee", "Customer" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create and add users to roles
            var users = new IdentityUser[]
                {
                    new IdentityUser { UserName = "julia.smith@gmail.com", Email = "julia.smith@gmail.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "michael.jones@hotmail.com", Email = "michael.jones@hotmail.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "sophie.martin@yahoo.com", Email = "sophie.martin@yahoo.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "alexander.wilson@outlook.com", Email = "alexander.wilson@outlook.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "lisa.anderson@gmail.com", Email = "lisa.anderson@gmail.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "chris.jackson@hotmail.com", Email = "chris.jackson@hotmail.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "emily.brown@yahoo.com", Email = "emily.brown@yahoo.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "david.miller@outlook.com", Email = "david.miller@outlook.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "olivia.davis@gmail.com", Email = "olivia.davis@gmail.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "benjamin.white@hotmail.com", Email = "benjamin.white@hotmail.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "ava.moore@yahoo.com", Email = "ava.moore@yahoo.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "jackson.lee@outlook.com", Email = "jackson.lee@outlook.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "mia.rodriguez@gmail.com", Email = "mia.rodriguez@gmail.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "ethan.harris@hotmail.com", Email = "ethan.harris@hotmail.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "chloe.martinez@yahoo.com", Email = "chloe.martinez@yahoo.com", EmailConfirmed = true }
                };


            foreach (var user in users)
            {
                var result = await userManager.CreateAsync(user, "Password123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            var employees = new IdentityUser[]
            {
                new IdentityUser { UserName = "josefin@mikaelsson.nu", Email = "josefin@mikaelsson.nu", EmailConfirmed = true }
                // Add more managers as needed
            };

            foreach (var employee in employees)
            {
                var result = await userManager.CreateAsync(employee, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employee, "Employee");
                }
            }

            // Save changes to get user IDs
            await context.SaveChangesAsync();

            // Create employees linked to the users
            var customers = new List<Customer>();
            foreach (var user in users)
            {
                customers.Add(new Customer
                {
                    ApplicationUserId = user.Id,
                    FirstName = user.UserName.Split('@')[0].Split('.')[0], // Extract first name from email
                    LastName = user.UserName.Split('@')[0].Split('.').Length > 1 ? user.UserName.Split('@')[0].Split('.')[1] : "Lastname",
                    Email = user.Email
                });
            }

            foreach (var employee in employees)
            {
                customers.Add(new Customer
                {
                    ApplicationUserId = employee.Id,
                    FirstName = employee.UserName.Split('@')[0].Split('.')[0], // Extract first name from email
                    LastName = employee.UserName.Split('@')[0].Split('.').Length > 1 ? employee.UserName.Split('@')[0].Split('.')[1] : "Lastname",
                    Email = employee.Email
                });
            }

            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }
    }
}