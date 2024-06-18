using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;

namespace LovingEssentials.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _productDAO;

        public ProductRepository(ProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        public async Task<List<ProductDTO>> FilterProducts(int brandId, int categoryId, string search)
        {
            return await _productDAO.FilterProducts(brandId, categoryId, search);
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            return await _productDAO.GetProducts();
        }
    }
}
