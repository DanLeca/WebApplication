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
    /*
    public static class Initializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                CreateUser(userManager, "Member1@email.com", "password", true);

                for (int i = 1; i <= 5; i++)
                {
                    CreateUser(userManager, "Customer" + i + "@email.com", "password", false);
                }
            }
        }

        public static void CreateUser(UserManager<ApplicationUser> userManager, String email, String password, bool member)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = email
            };
            var hasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hasher.HashPassword(user, password);

            userManager.CreateAsync(user).Wait();


            var claims = new List<Claim>();
            claims.Add(new Claim("comment_new", "true"));
            if (member)
            {
                // TODO: Self permissions (delete own posts)
                // Post stuff
                claims.Add(new Claim("post_new", "true"));
                claims.Add(new Claim("post_edit", "true"));
                claims.Add(new Claim("post_delete", "true"));
                // Comment stuff
                // Admin stuff
                claims.Add(new Claim("admin_analytics", "true"));
            }
            userManager.AddClaimsAsync(user, claims).Wait();
        }
    }
    */

    public class Initializer
    {
        public static void Initialize(UserManager <ApplicationUser> userManager, ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = "Customer1@email.com",
                    UserName = "Customer1@email.com"
                },
                user2 = new ApplicationUser
                {
                    Email = "Customer2@email.com",
                    UserName = "Customer2@email.com"
                },
                user3 = new ApplicationUser
                {
                    Email = "Customer3@email.com",
                    UserName = "Customer3@email.com"
                },
                user4 = new ApplicationUser
                {
                    Email = "Customer4@email.com",
                    UserName = "Customer4@email.com"
                },
                user5 = new ApplicationUser
                {
                    Email = "Customer5@email.com",
                    UserName = "Customer5@email.com"
                },
                user6 = new ApplicationUser
                {
                    Email = "Member1@email.com",
                    UserName = "Member1@email.com"
                };

                var hasher = new PasswordHasher<ApplicationUser>();

                user.PasswordHash = hasher.HashPassword(user, "password");
                userManager.CreateAsync(user).Wait();

                user2.PasswordHash = hasher.HashPassword(user2, "password");
                userManager.CreateAsync(user2).Wait();

                user3.PasswordHash = hasher.HashPassword(user3, "password");
                userManager.CreateAsync(user3).Wait();

                user4.PasswordHash = hasher.HashPassword(user4, "password");
                userManager.CreateAsync(user4).Wait();

                user5.PasswordHash = hasher.HashPassword(user5, "password");
                userManager.CreateAsync(user5).Wait();

                user6.PasswordHash = hasher.HashPassword(user6, "password");
                userManager.CreateAsync(user6).Wait();
            }
            context.SaveChanges();

            /*
            if (context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = "Customer1@email.com",
                    UserName = "Customer1@email.com"
                };
                var hasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = hasher.HashPassword(user, "password");
                userManager.CreateAsync(user).Wait();
            }

            */

        }
    }
}