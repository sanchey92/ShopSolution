using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ShopSolution.Infrastructure.Data;

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

            return services;
        }
    }
}