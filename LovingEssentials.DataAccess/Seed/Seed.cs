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
    }
}
