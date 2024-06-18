using LovingEssentials.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.Seed
{
    public class Seed
    {
        public static async Task SeedUser(DataContext _context)
        {
            if (await _context.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("../LovingEssentials.DataAccess/Seed/UserSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<User>>(userData, jsonOptions);

            foreach (var user in users)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task CategorySeed(DataContext _context)
        {
            if (await _context.Categories.AnyAsync()) { return; }

            var list = new List<Category>
            {
                new Category {Name="Chilren Milk"},
                new Category {Name="Baby Milk"},
                new Category {Name="Dairy Cow"},
                new Category {Name="Pregnant Milk"}
            };

            foreach (var i in list)
            {
                await _context.Categories.AddAsync(i);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task SeedBranch(DataContext _context)
        {
            if (await _context.Brands.AnyAsync()) { return; }

            var list = new List<Brand>
            {
                new Brand {Name="Nestle"},
                new Brand {Name="Meiji"},
                new Brand {Name="Abbott"},
                new Brand {Name="Enfa"},
                new Brand {Name="Dutch Lady"},
                new Brand {Name="NutiFood"},
                new Brand {Name="TH True Milk"},
                new Brand {Name="Friso"}
            };

            foreach (var i in list)
            {
                await _context.Brands.AddAsync(i);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task SeedProduct(DataContext _context)
        {
            if (await _context.Products.AnyAsync()) return;

            var productData = await File.ReadAllTextAsync("../LovingEssentials.DataAccess/Seed/ProductSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var products = JsonSerializer.Deserialize<List<Product>>(productData, jsonOptions);

            foreach (var product in products)
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
