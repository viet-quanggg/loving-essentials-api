using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.Repository.IRepository;

namespace LovingEssentials.Repository.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly BrandDAO _brandDAO;

        public BrandRepository(BrandDAO brandDAO)
        {
            _brandDAO = brandDAO;
        }
        public async Task<List<Brand>> GetBrands()
        {
            return await _brandDAO.GetBrands();
        }
    }
}
