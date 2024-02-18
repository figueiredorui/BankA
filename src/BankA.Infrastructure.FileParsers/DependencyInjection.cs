using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankA.Infrastructure.FileParsers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBankAInfrastructureFileParsers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<BankA.Application.Interfaces.IStatementFileParserFactory, BankA.Infrastructure.FileParsers.StatementFileFormatFactory>();

            return services;
        }
    }


}
