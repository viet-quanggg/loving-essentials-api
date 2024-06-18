using LovingEssentials.BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace LovingEssentials.DataAccess.DAOs
{
    public class CategoryDAO
    {
        private readonly DataContext _context;

        public CategoryDAO(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategories()
        {
            try
            {
                var result = await _context.Categories.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
