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
    public async Task<IActionResult> AddAddress([FromForm] CreateAddressDTO createAddressDto)
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
}