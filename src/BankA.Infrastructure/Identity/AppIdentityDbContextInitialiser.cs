using BankA.Infrastructure.Identity.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Identity
{
    public static class AppIdentityDbContextInitialiserExtensions
    {
        public static void AppIdentityDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<AppIdentityDbContextInitialiser>();

            Task.WaitAll(initialiser.InitialiseAsync());
            Task.WaitAll(initialiser.SeedAsync());
        }
    }

    public class AppIdentityDbContextInitialiser
    {
        private readonly ILogger<AppIdentityDbContextInitialiser> _logger;
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppIdentityDbContextInitialiser(ILogger<AppIdentityDbContextInitialiser> logger, AppIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (!_context.Database.ProviderName.Contains("InMemory"))
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
                await ApplicationUserSeed.SeedAsync(_userManager);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
    }
}