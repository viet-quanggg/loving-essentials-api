using LovingEssentials.BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace LovingEssentials.DataAccess.DAOs
{
    public class BrandDAO
    {
        private readonly DataContext _context;

        public BrandDAO(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Brand>> GetBrands()
        {
            try
            {
                var result = await _context.Brands.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
