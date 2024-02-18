using BankA.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankA.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBankAApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CategoriseTransactionsService>();
            services.AddScoped<PeriodFilterService>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            return services;
        }
    }
}
