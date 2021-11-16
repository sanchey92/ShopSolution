using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ShopSolution.Infrastructure.Data;
using ShopSolution.Infrastructure.Identity;
using StackExchange.Redis;

namespace ShopSolution.API.Extensions
{
    public static class DatabaseServicesExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services,
            IConfiguration configuration)
        {
            var defaultBuilder = CreateBuilder(configuration, "DefaultConnection");
            var identityBuilder = CreateBuilder(configuration, "IdentityConnection");

            services.AddDbContext<StoreContext>(options =>
                options.UseNpgsql(defaultBuilder.ConnectionString));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseNpgsql(identityBuilder.ConnectionString));

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var config = ConfigurationOptions.Parse(configuration
                    .GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(config);
            });

            return services;
        }

        private static NpgsqlConnectionStringBuilder CreateBuilder(
            IConfiguration configuration,string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                ConnectionString = configuration.GetConnectionString(connectionString),
                Username = configuration["User ID"],
                Password = configuration["Password"]
            };

            return builder;
        }
    }
}