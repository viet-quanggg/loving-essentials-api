using AutoMapper;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LovingEssentials.DataAccess.DAOs;

public class AddressDAO
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AddressDAO(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<CreateAddressDTO> CreateAddress(CreateAddressDTO createAddressDto)
    {
        if (createAddressDto != null)
        {
            var address = _mapper.Map<Address>(createAddressDto);
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == createAddressDto.UserAddress.Id);
            if(user != null)
            { 
                address.Users = user;
            }
            try
            {
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
                return createAddressDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }
        
        return null;
    }

    public async  Task<List<UserAddressDTO>> GetAddressByUser(int userId)
    {
        if (userId != null)
        {
            var address = await  _context.Addresses
                .Include(a => a.Users)
                .Where(u => u.Users.Id == userId).ToListAsync();
            var dto = _mapper.Map<List<UserAddressDTO>>(address);

            return dto;
        }

        throw new ArgumentException();
    }
}