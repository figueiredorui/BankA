using BankA.Infrastructure.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Data
{
    public static class AppDataDbContextInitialiserExtensions
    {
        public static void AppDataDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<AppDataDbContextInitialiser>();

            Task.WaitAll(initialiser.InitialiseAsync());
            Task.WaitAll(initialiser.SeedAsync());
        }
    }

    public class AppDataDbContextInitialiser
    {
        private readonly ILogger<AppDataDbContextInitialiser> _logger;
        private readonly AppDataDbContext _context;

        public AppDataDbContextInitialiser(ILogger<AppDataDbContextInitialiser> logger, AppDataDbContext context)
        {
            _logger = logger;
            _context = context;
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
                await ApplicationUserSeed.SeedAsync(_context);
                await MerchantSeed.SeedAsync(_context);
                await CategorySeed.SeedAsync(_context);
                await RuleSeed.SeedAsync(_context);
                await TransactionSeed.SeedAsync(_context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
    }
}