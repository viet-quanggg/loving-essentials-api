using LovingEssentials.DataAccess.DTOs.Admin;
using LovingEssentials.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LovingEssentials.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _repository;

        public StoreController(IStoreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<StoreDTO>>> GetStores()
        {
            var result = await _repository.GetStores();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDTO>> GetStoreById(int id)
        {
            var result = await _repository.GetStoreById(id);

            if(result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore(CreateStoreDTO store)
        {
            var result = await _repository.CreateStore(store);
            
            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateStore(StoreDTO store)
        {
            var result = await _repository.UpdateStore(store);
            
            if(!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _repository.DeleteStore(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
