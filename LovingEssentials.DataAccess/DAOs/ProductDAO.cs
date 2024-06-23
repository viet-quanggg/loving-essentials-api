using AutoMapper;
using AutoMapper.QueryableExtensions;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
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
                var result = _context.Products.AsQueryable();
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
    }
}
