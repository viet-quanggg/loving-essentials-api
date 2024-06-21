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
}