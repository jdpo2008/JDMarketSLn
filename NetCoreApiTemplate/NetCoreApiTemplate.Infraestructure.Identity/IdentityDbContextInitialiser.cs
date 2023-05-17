using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using JDMarketSLn.Application.Common.Enums;
using JDMarketSLn.Domain.Entities.Identity;
using JDMarketSLn.Infraestructure.Identity.Contexts;
using System.Security.Claims;

namespace CleanArchitecture.Infrastructure.Persistence;

public class IdentityDbContextInitialiser
{
    private readonly ILogger<IdentityDbContextInitialiser> _logger;
    private readonly IdentityContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public IdentityDbContextInitialiser(ILogger<IdentityDbContextInitialiser> logger, IdentityContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var superAdmin = new ApplicationRole(Roles.SuperAdmin.ToString());

        if (_roleManager.Roles.All(r => r.Name != superAdmin.Name))
        {
            await _roleManager.CreateAsync(superAdmin);

            await _roleManager.AddClaimAsync(superAdmin, new Claim("permission", "get"));
            await _roleManager.AddClaimAsync(superAdmin, new Claim("permission", "create"));
            await _roleManager.AddClaimAsync(superAdmin, new Claim("permission", "update"));
            await _roleManager.AddClaimAsync(superAdmin, new Claim("permission", "delete"));
        }

        var admin = new ApplicationRole(Roles.Admin.ToString());

        if (_roleManager.Roles.All(r => r.Name != admin.Name))
        {
            await _roleManager.CreateAsync(admin);

            await _roleManager.AddClaimAsync(admin, new Claim("permission", "get"));
            await _roleManager.AddClaimAsync(admin, new Claim("permission", "create"));
            await _roleManager.AddClaimAsync(admin, new Claim("permission", "update"));
        }

        var basic = new ApplicationRole(Roles.Basic.ToString());

        if (_roleManager.Roles.All(r => r.Name != basic.Name))
        {
            await _roleManager.CreateAsync(basic);

            await _roleManager.AddClaimAsync(basic, new Claim("permission", "get"));
        }

        // Default users
        var administrator = new ApplicationUser 
        { 
            FirstName = "José", 
            LastName = "Perez",  
            UserName = "jdpo2008", 
            Email = "jdpo2008@gmail.com", 
            PhoneNumber = "+51910380781",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (_userManager.Users.All(u => u.Email != administrator.Email))
        {
            await _userManager.CreateAsync(administrator, "123Pa$$word!");

            await _userManager.AddToRoleAsync(administrator, Roles.SuperAdmin.ToString());
            await _userManager.AddToRoleAsync(administrator, Roles.Admin.ToString());
            await _userManager.AddToRoleAsync(administrator, Roles.Basic.ToString());
        }

    }
}
