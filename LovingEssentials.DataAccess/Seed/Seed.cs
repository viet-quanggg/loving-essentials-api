using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DAOs;
using Microsoft.AspNetCore.Identity;
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
        
        public static async Task SeedUser(DataContext _context, IPasswordHasher<User> _passwordHasher)
        {
            if (await _context.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("../LovingEssentials.DataAccess/Seed/UserSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<User>>(userData, jsonOptions);

            foreach (var user in users)
            {
                user.Password = HashPassword(_passwordHasher, user, user.Password);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        private static string HashPassword(IPasswordHasher<User> _passwordHasher, User account, string password)
        {
            return _passwordHasher.HashPassword(account, password);
        }
        public static async Task SeedAddress(DataContext _context)
        {
            if(await _context.Addresses.AnyAsync()) { return; }

            var addressData = await File.ReadAllTextAsync("../LovingEssentials.DataAccess/Seed/AddressSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var addresses = JsonSerializer.Deserialize<List<Address>>(addressData, jsonOptions);
            foreach (var address in addresses)
            {
                var user = await  _context.Users.Where(u => u.Id == 1).FirstOrDefaultAsync();
                address.Users = user;
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
            }
        }
      

        public static async Task SeedBrand(DataContext _context)
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
        public static async Task SeedCategory(DataContext _context)
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
        public static async Task SeedProduct(DataContext _context)
        {
            if (await _context.Products.AnyAsync()) { return; }

            var milk = await File.ReadAllTextAsync("../LovingEssentials.DataAccess/Seed/ProductSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var m = JsonSerializer.Deserialize<List<Product>>(milk, jsonOptions);

            foreach (var i in m)
            {
                await _context.Products.AddAsync(i);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task SeedOrders(DataContext _context)
        {
            if (await _context.Orders.AnyAsync()) { return; }

            var order = await File.ReadAllTextAsync("../LovingEssentials.DataAccess/Seed/OrderSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var m = JsonSerializer.Deserialize<List<Order>>(order, jsonOptions);

            foreach (var i in m)
            {
                await _context.Orders.AddAsync(i);
                await _context.SaveChangesAsync();
            }
        }
    }
}
