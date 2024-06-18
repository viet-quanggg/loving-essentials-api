using LovingEssentials.BusinessObject;

namespace LovingEssentials.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    }
}
