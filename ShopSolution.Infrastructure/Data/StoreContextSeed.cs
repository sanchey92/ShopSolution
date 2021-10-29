using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopSolution.Core.Entities;

namespace ShopSolution.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData =
                        File.ReadAllText("../ShopSolution.Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                        context.ProductBrands.Add(item);

                    await context.SaveChangesAsync();
                }
                
                if (!context.ProductTypes.Any())
                {
                    var typesData =
                        File.ReadAllText("../ShopSolution.Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                        context.ProductTypes.Add(item);

                    await context.SaveChangesAsync();
                }
                
                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText("../ShopSolution.Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                        context.Products.Add(item);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(exception.Message);
            }
        }
    }
}