using LovingEssentials.BusinessObject;

namespace LovingEssentials.Repository.IRepository
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetBrands();
    }
}
