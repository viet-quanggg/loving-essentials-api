using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Admin;

namespace LovingEssentials.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductDTO>> GetProducts();
        Task<List<ProductDTO>> FilterProducts(int brandId, int categoryId, string search);
        Task<ProductDTO> GetProductbyId(int Id);
        Task<Product> GetProductbyIdAdmin(int id);
        Task<bool> CreateProduct(CreateProductDTO createProductDTO);
        Task<bool> EditProduct(EditProductDTO editProductDTO);
        Task<bool> DeleteProduct(int id);
    }
}
