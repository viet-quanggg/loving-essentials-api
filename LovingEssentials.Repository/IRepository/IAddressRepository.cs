using LovingEssentials.DataAccess.DTOs;

namespace LovingEssentials.Repository.IRepository;

public interface IAddressRepository
{
    Task<CreateAddressDTO> AddAddress(CreateAddressDTO createAddressDto);
    Task<List<UserAddressDTO>> GetAddressByUser(int userId);
    Task<UserAddressDTO> GetAddressById(int addId);

    Task<bool> UpdateAddress(UpdateAddressDto updateAddressDto);

    Task<bool> DeleteAddress(int addId);

}