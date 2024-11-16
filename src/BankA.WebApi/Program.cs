using BankA.Application;
using BankA.Infrastructure;
using BankA.Infrastructure.Data;
using BankA.Infrastructure.FileParsers;
using BankA.Infrastructure.Identity;
using DotNetLib.AspNetCore.Filters;
using DotNetLib.AspNetCore.Handlers;
using DotNetLib.Serilog;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Serilog;
using System.Text.Json.Serialization;

namespace BankA.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = SerilogExtensions.CreateLoggerConfiguration();

        try
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationInsightsTelemetry();

            // Add services to the container.

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

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddBankAApplication(builder.Configuration);
            builder.Services.AddBankAInfrastructure(builder.Configuration);
            builder.Services.AddBankAInfrastructureFileParsers(builder.Configuration);

            builder.Host.UseSerilog();

            var app = builder.Build();

            /******************************************/
            // Configure the HTTP request pipeline.

            app.AppIdentityDatabaseAsync();
            //app.AppDataDatabaseAsync();

            app.UseDefaultFiles();
            app.MapStaticAssets();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers().RequireAuthorization();

            app.MapGroup("/api/identity")
                    .WithTags("Identity")
                    .MapIdentityApi<ApplicationUser>();

            app.MapFallbackToFile("/index.html");

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
