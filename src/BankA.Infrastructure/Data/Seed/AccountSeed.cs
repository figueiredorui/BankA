using BankA.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Data.Seed
{
    internal class ApplicationUserSeed
    {
        internal static async Task SeedAsync(AppDataDbContext context)
        {
            var count = context.Accounts.Count();
            if (count > 0)
                return;

            await context.Accounts.AddRangeAsync(
                new Account("Demo Account", "CSV"),
                new Account("Personal Account", "CSV")
                );

            await context.SaveChangesAsync();
        }
    }
}
