using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;

namespace LovingEssentials.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductDTO>> GetProducts();
        Task<List<ProductDTO>> FilterProducts(int brandId, int categoryId, string search);
        Task<ProductDTO> GetProductbyId(int Id);
    }
}
