using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.Repository.IRepository;

namespace LovingEssentials.Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO;

        public CategoryRepository(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _categoryDAO.GetCategories();
        }
    }
}
