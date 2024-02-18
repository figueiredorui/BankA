using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;

namespace BankA.Infrastructure.Data
{

    public class AppDataDbContextFactory : IDesignTimeDbContextFactory<AppDataDbContext>
    {

        AppDataDbContext IDesignTimeDbContextFactory<AppDataDbContext>.CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BankA.WebApp"))
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<AppDataDbContext>();

            builder.UseSqlServer(connectionString);

            return new AppDataDbContext(builder.Options, null);
        }
    }

}
