using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.Repository.IRepository
{
    public interface IStoreRepository
    {
        Task<List<StoreDTO>> GetStores();
        Task<StoreDTO> GetStoreById(int id);
        Task<bool> CreateStore(CreateStoreDTO createStoreDTO);
        Task<bool> UpdateStore(StoreDTO updateStoreDTO);
        Task<bool> DeleteStore(int id);
    }
}
