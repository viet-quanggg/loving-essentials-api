using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LovingEssentials.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            var result = await _productRepository.GetProducts();
            return result;
        }
        [HttpGet("filter")]
        public async Task<ActionResult<List<ProductDTO>>> GetFilteredProducts(int brandId, int categoryId, string? search)
        {
            var result = await _productRepository.FilterProducts(brandId, categoryId, search);
            return result;
        }
    }
}
