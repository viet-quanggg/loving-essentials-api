using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Admin;
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
        [HttpGet("detail")]
        public async Task<ActionResult<ProductDTO>> GetProductbyId(int Id)
        {
            var result = await _productRepository.GetProductbyId(Id);
            return result;
        }
        [HttpGet("detail-admin")]
        public async Task<ActionResult<Product>> GetProductbyIdAdmin(int id)
        {
            var result = await _productRepository.GetProductbyIdAdmin(id);
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAdmin(CreateProductDTO createProductDTO)
        {
            var result = await _productRepository.CreateProduct(createProductDTO);
            if(!result) return BadRequest();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductAdmin(EditProductDTO editProductDTO)
        {
            var result = await _productRepository.EditProduct(editProductDTO);
            if (!result) return BadRequest();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAdmin(int id)
        {
            var result = await _productRepository.DeleteProduct(id);
            if (!result) return BadRequest();
            return Ok();
        }

    }
}
