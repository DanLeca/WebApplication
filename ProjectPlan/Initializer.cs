using Microsoft.AspNetCore.Identity;
using ProjectPlan.Data;
using ProjectPlan.Models;
using System.Diagnostics;
using System.Linq;
using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace ProjectPlan
{
    public class Initializer
    {
        public static void Initialize(UserManager <ApplicationUser> userManager, ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                CreateUser(userManager, "Member1@email.com", "password");

                for (int i = 1; i <= 5; i++)
                {
                    CreateUser(userManager, "Customer" + i + "@email.com", "password");
                }
            }

            if (context.Users.Any())
            {
                CreateUser(userManager, "Member1@email.com", "password");

                for (int i = 1; i <= 5; i++)
                {
                    CreateUser(userManager, "Customer" + i + "@email.com", "password");
                }
            }
        }

        public static void CreateUser(UserManager<ApplicationUser> userManager, String email, String password)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = email
            };
            var hasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hasher.HashPassword(user, password);

            userManager.CreateAsync(user).Wait();
        }


    }
}