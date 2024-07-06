using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.DataAccess.DTOs.Admin;
using LovingEssentials.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.Repository.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDAO _storeDAO;

        public StoreRepository(StoreDAO storeDAO)
        {
            _storeDAO = storeDAO;
        }

        public async Task<StoreDTO> GetStoreById(int id)
        {
            return await _storeDAO.GetStoreById(id);
        }

        public async Task<List<StoreDTO>> GetStores()
        {
            return await _storeDAO.GetStores();
        }

        public async Task<bool> CreateStore(CreateStoreDTO createStoreDTO)
        {
            return await _storeDAO.CreateStore(createStoreDTO);
        }

        public async Task<bool> UpdateStore(StoreDTO updateStoreDTO)
        {
            return await _storeDAO.UpdateStore(updateStoreDTO);
        }

        public async Task<bool> DeleteStore(int id)
        {
            return await _storeDAO.DeleteStore(id);
        }
    }
}
