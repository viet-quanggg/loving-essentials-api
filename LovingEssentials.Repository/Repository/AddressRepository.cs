using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;

namespace LovingEssentials.Repository.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly AddressDAO _addressDao;

    public AddressRepository(AddressDAO addressDao)
    {
        _addressDao = addressDao;
    }
    
    public async Task<CreateAddressDTO> AddAddress(CreateAddressDTO createAddressDto)
    {
        if (createAddressDto != null)
        {
            await  _addressDao.CreateAddress(createAddressDto);
            return createAddressDto;
        }
        return null;
    }

    public async Task<List<UserAddressDTO>> GetAddressByUser(int userId)
    {
        if (userId != null)
        {
          var addresses = await  _addressDao.GetAddressByUser(userId);
          return addresses;
        }
        return null;
    }

    public async Task<UserAddressDTO> GetAddressById(int addId)
    {
        if (addId != null)
        {
            var addresses = await  _addressDao.GetAddressById(addId);
            return addresses;
        }
        return null;
    }

    public async Task<bool> UpdateAddress(UpdateAddressDto updateAddressDto)
    {
        if (updateAddressDto != null)
        {
          await _addressDao.UpdateAddress(updateAddressDto);
          return true;
        }

        return false;
    }

    public async Task<bool> DeleteAddress(int addId)
    {
        if (addId != null)
        {
            await _addressDao.DeleteAddress(addId);
            return true;
        }
        return false;
    }
}