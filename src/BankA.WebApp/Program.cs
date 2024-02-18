
using BankA.Application;
using BankA.Infrastructure;
using BankA.Infrastructure.Data;
using BankA.Infrastructure.FileParsers;
using BankA.Infrastructure.Identity;
using DotNetLib.AspNetCore.Filters;
using DotNetLib.AspNetCore.Handlers;
using DotNetLib.Serilog;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json.Serialization;

namespace BankA.WebApp8
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = SerilogExtensions.CreateLoggerConfiguration();

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddControllers(o =>
                {
                    o.Filters.Add(typeof(ModelValidationFilter));
                });

                builder.Services.Configure<JsonOptions>(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

                builder.Services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                });

                builder.Services.AddExceptionHandler<CustomExceptionHandler>();

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddBankAApplication(builder.Configuration);
                builder.Services.AddBankAInfrastructure(builder.Configuration);
                builder.Services.AddBankAInfrastructureFileParsers(builder.Configuration);

                if (builder.Environment.IsProduction())
                {
                    builder.Services.AddSpaStaticFiles(configuration =>
                    {
                        configuration.RootPath = "ClientApp/dist/banka/browser";
                    });
                }

                builder.Host.UseSerilog();

                var app = builder.Build();

                /******************************************/
                // Configure the HTTP request pipeline.

                app.AppIdentityDatabaseAsync();
                app.AppDataDatabaseAsync();

                app.UseSwagger();
                app.UseSwaggerUI();

                if (app.Environment.IsProduction())
                {
                    app.UseHsts();
                }

                app.UseHealthChecks("/api/health");
                app.UseHttpsRedirection();
                app.UseStaticFiles();

                if (app.Environment.IsProduction())
                {
                    app.UseSpaStaticFiles();
                }

                app.UseAuthorization();

                app.UseExceptionHandler(options => { });

                app.UseMiddleware<IdentityApiRestrictionsMiddleware>();

                app.MapControllers().RequireAuthorization();

                app.MapGroup("/api/identity")
                        .WithTags("Identity")
                        .MapIdentityApi<ApplicationUser>();

                app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
                {
                    builder.UseSpa(spa =>
                    {
                        spa.Options.SourcePath = "ClientApp/dist/banka/browser";

                        if (app.Environment.IsDevelopment())
                        {
                            //spa.UseAngularCliServer(npmScript: "start");
                            spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                        }
                    });
                });

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
