using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Admin;
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
        public async Task<List<ProductDTO>> GetProductsAdmin()
        {
            return await _productDAO.GetProductsForAdmin();
        }

        public async Task<ProductDTO> GetProductbyId(int Id)
        {
            return await _productDAO.getProductbyId(Id);
        }
        public async Task<Product> GetProductbyIdAdmin(int id)
        {
            return await _productDAO.GetProductbyIdAdmin(id);
        }

        public async Task<bool> CreateProduct(CreateProductDTO createProductDTO)
        {
            return await _productDAO.CreateProduct(createProductDTO);
        }

        public async Task<bool> EditProduct(EditProductDTO editProductDTO)
        {
            return await _productDAO.EditProduct(editProductDTO);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productDAO.DeleteProduct(id);
        }
    }
}
