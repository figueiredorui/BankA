using BankA.Application.Interfaces;
using BankA.Infrastructure.Data;
using BankA.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankA.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBankAInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityUserId, IdentityCurrentUser>();

            services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    opt.UseInMemoryDatabase("BankA");
                }
                else
                {
                    opt.UseSqlServer(connectionString, o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName + "_identity", "dbo"));
                }
            });
            services.AddScoped<AppIdentityDbContextInitialiser>();

            services.AddIdentityCore<ApplicationUser>()
                    //.AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddApiEndpoints();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<IAppDataDbContext, AppDataDbContext>();
            services.AddDbContext<AppDataDbContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    opt.UseInMemoryDatabase("BankA");
                }
                else
                {
                    opt.UseSqlServer(connectionString, o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName + "_main", "dbo"));
                }
            });
            services.AddScoped<AppDataDbContextInitialiser>();

            services.AddHealthChecks()
                        .AddDbContextCheck<AppIdentityDbContext>();

            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                        .AddCookie(IdentityConstants.ApplicationScheme, options =>
                         {
                             options.ExpireTimeSpan = new System.TimeSpan(24, 0, 0);
                             options.Events.OnRedirectToLogin = (context) =>
                             {
                                 context.Response.StatusCode = 401;
                                 return Task.CompletedTask;
                             };
                         });

            services.AddAuthorization();

            return services;
        }
    }


}
