using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LovingEssentials.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressRepository _addressRepository;

    public AddressController(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    [HttpPost("/api/Address/Add-Address")]
    public async Task<IActionResult> AddAddress([FromBody] CreateAddressDTO createAddressDto)
    {
        return Ok(await _addressRepository.AddAddress(createAddressDto));
    }

    [HttpGet("/api/Address/GetAddressByUser/{userId}")]
    public async Task<IActionResult> GetAddressByUser(int userId)
    {
        if (userId != null)
        {
            return Ok(await _addressRepository.GetAddressByUser(userId));
        }

        return BadRequest("User Id is null");
    }
    
    [HttpGet("/api/Address/GetAddressById/{addId}")]
    public async Task<IActionResult> GetAddressById(int addId)
    {
        if (addId != null)
        {
            return Ok(await _addressRepository.GetAddressById(addId));
        }

        return BadRequest("Address Id is null");
    }

    [HttpDelete("/api/Address/DeleteAddress/{addId}")]
    public async Task<IActionResult> DeleteAddress(int addId)
    {
        if (addId != null)
        {
            return Ok(await _addressRepository.DeleteAddress(addId));
        }

        return BadRequest("Address ID is null");
    }
    
    [HttpPut("/api/Address/UpdateAddress")]
    public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressDto updateAddressDto)
    {
        if (updateAddressDto != null)
        {
            return Ok(await _addressRepository.UpdateAddress(updateAddressDto));
        }

        return BadRequest("Address is null");
    }
}