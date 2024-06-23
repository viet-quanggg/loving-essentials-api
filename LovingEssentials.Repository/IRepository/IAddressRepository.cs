using LovingEssentials.DataAccess.DTOs;

namespace LovingEssentials.Repository.IRepository;

public interface IAddressRepository
{
    Task<CreateAddressDTO> AddAddress(CreateAddressDTO createAddressDto);
    Task<List<UserAddressDTO>> GetAddressByUser(int userId);
}