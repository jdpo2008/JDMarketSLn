using JdMarketSln.Application.Enums;
using JdMarketSln.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new Role
            {
                Name = Roles.SuperAdmin.ToString(),
                Description = "Role - Super Admin",
            });
            await roleManager.CreateAsync(new Role
            {
                Name = Roles.Admin.ToString(),
                Description = "Role - Admin",
            });
            await roleManager.CreateAsync(new Role
            {
                Name = Roles.Moderator.ToString(),
                Description = "Role - Moderator",
            });
            await roleManager.CreateAsync(new Role
            {
                Name = Roles.Basic.ToString(),
                Description = "Role - Basic",
            });
        }
    }
}
