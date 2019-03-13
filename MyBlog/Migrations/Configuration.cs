namespace MyBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyBlog.Models;
    using MyBlog.Models.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Manager for manage our roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Manager to manage our users
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Do not change these names 
            const string ADMIN = "Admin";
            const string MODERATOR = "Moderator";

            ApplicationUser adminUser;

            CreatePositionFor(ADMIN);

            CreatePositionFor(MODERATOR);


            //creating position in our database by using this method
            //passing a role as parameter
            void CreatePositionFor(string position)
            {
                if (!context.Roles.Any(p => p.Name == position))
                {
                    var adminRole = new IdentityRole(position);
                    roleManager.Create(adminRole);
                }

                if (!context.Users.Any(p => p.UserName == position + "@blog.com"))
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = position.ToLower() + "@blog.com",
                        Email = position.ToLower() + "@blog.com",
                    };
                    userManager.Create(adminUser, "Password-1");
                }
                else
                {
                    adminUser = context.Users.First(p => p.UserName == position.ToLower() + "@blog.com");
                }

                if (!userManager.IsInRole(adminUser.Id, position))
                {
                    userManager.AddToRole(adminUser.Id, position);
                }
            }
        }

    }
}
