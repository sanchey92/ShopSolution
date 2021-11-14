using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ShopSolution.Infrastructure.Data;
using StackExchange.Redis;

namespace ShopSolution.API.Extensions
{
    public static class DatabaseServicesExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services,
            IConfiguration configuration)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                Username = configuration["User ID"],
                Password = configuration["Password"]
            };

            services.AddDbContext<StoreContext>(options =>
                options.UseNpgsql(builder.ConnectionString));

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var config = ConfigurationOptions.Parse(configuration
                    .GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(config);
            });

            return services;
        }
    }
}