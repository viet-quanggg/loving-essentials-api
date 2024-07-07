using AutoMapper;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DAOs;

public class StoreDAO
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public StoreDAO(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<StoreDTO>> GetStores()
    {
        try
        {
            var store = await _context.Stores.ToListAsync();
            var storeDto = _mapper.Map<List<StoreDTO>>(store);
            return storeDto;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<StoreDTO> GetStoreById(int id)
    {
        if (id != null)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);
            var storeDto = _mapper.Map<StoreDTO>(store);
            return storeDto;
        }

        throw new ArgumentException();
    }

    public async Task<bool> CreateStore(CreateStoreDTO store)
    {
        var newStore = _mapper.Map<Store>(store);

        try
        {
            _context.Stores.Add(newStore);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UpdateStore(StoreDTO store)
    {
        var updateStore = await _context.Stores.FirstOrDefaultAsync(s => s.Id == store.Id);

        try
        {
            if (updateStore == null)
            {
                return false;
            }

            updateStore.Name = store.Name;
            updateStore.Latitude = store.Latitude;
            updateStore.Longitude = store.Longitude;
            updateStore.Address = store.Address;
            updateStore.Phone = store.Phone;
            updateStore.OpenHours = store.OpenHours;
            updateStore.CloseHours = store.CloseHours;

            _context.Stores.Update(updateStore);
            var result = await _context.SaveChangesAsync() > 0;

            var updatedStoreDto = _mapper.Map<StoreDTO>(updateStore);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteStore(int id)
    {
        var deleteStore = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);

        try
        {
            if (deleteStore == null)
            {
                return false;
            }

            _context.Stores.Remove(deleteStore);
            var result = await _context.SaveChangesAsync() > 0;

            var deletedStoreDto = _mapper.Map<StoreDTO>(deleteStore);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
