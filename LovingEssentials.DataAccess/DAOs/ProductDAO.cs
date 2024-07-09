using AutoMapper;
using AutoMapper.QueryableExtensions;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Admin;
using Microsoft.EntityFrameworkCore;

namespace LovingEssentials.DataAccess.DAOs
{
    public class ProductDAO
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductDAO(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            try
            {
                var result = await _context.Products
                    .Where(x => x.Status == 1)
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<ProductDTO>> FilterProducts(int brandId, int categoryId, string search)
        {
            try
            {
                var result = _context.Products.Where(x => x.Status == 1).AsQueryable();
                if(!string.IsNullOrEmpty(search))
                {
                    result = result.Where(x => x.Name.Contains(search));

                } else if(brandId != 0 || categoryId != 0)
                {
                    result = result.Where(x => x.BrandId == brandId || x.CategoryId == categoryId);

                } 

                return await result.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ProductDTO> getProductbyId(int id)
        {
            try { 
                var result = await _context.Products
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                    .Where(p => p.Id == id).FirstOrDefaultAsync();
                return result; 
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<Product> GetProductbyIdAdmin(int id)
        {
            try
            {
                var result = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id);
                return result;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<List<ProductDTO>> GetProductsForAdmin()
        {
            try
            {
                var result = await _context.Products
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateProduct(CreateProductDTO createProductDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(createProductDTO);
                await _context.Products.AddAsync(product);
                var result = await _context.SaveChangesAsync() > 0;
                return result;
            } catch
            {
                return false;
            }
            
        }
        public async Task<bool> EditProduct(EditProductDTO editProductDTO)
        {
            try
            {
                var existedProduct = await _context.Products.FindAsync(editProductDTO.Id);
                if (existedProduct != null)
                {
                    existedProduct.Name = editProductDTO.Name;
                    existedProduct.Quantity = editProductDTO.Quantity;
                    existedProduct.Price = editProductDTO.Price;
                    existedProduct.Description = editProductDTO.Description;
                    existedProduct.CategoryId = editProductDTO.CategoryId;
                    existedProduct.BrandId = editProductDTO.BrandId;
                    existedProduct.ImageURL = editProductDTO.ImageURL;
                    existedProduct.Status = editProductDTO.Status;
                }
                var result = await _context.SaveChangesAsync() > 0;
                return result;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var existedProduct = await _context.Products.FindAsync(id);
                if (existedProduct != null)
                {
                    existedProduct.Status = 0;
                }
                var result = await _context.SaveChangesAsync() > 0;
                return result;
            }
            catch
            {
                return false;
            }
        }

    }
}
