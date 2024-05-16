using FruitFactsApp.Library.Models.Entities;
using FruitFactsApp.Server.Data;
using FruitFactsApp.Server.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitFactsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        private readonly IFruitsRepository _repository;

        public FruitsController(IFruitsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitEntity>>> GetFruits()
        {
            try
            {
                return Ok(await _repository.GetAllFruits());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrevieng rom database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FruitEntity>> GetFruitById(int id)
        {
            try
            {
                var fruit = await _repository.GetFruitById(id);
                if(fruit is null)
                {
                    return NotFound("No such fruit found");
                }
                else
                {
                    return Ok(fruit);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while retrevieng fruit id: {id} from database");
            }

        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<FruitEntity>> GetFruitByName(string name)
        {
            try
            {
                var fruit = await _repository.GetFruitByName(name);
                if(fruit is null)
                {
                    return NotFound("No fruit of such name has been found good sire");
                }
                else
                {
                    return Ok(fruit);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while looking for fruit named: {name}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<FruitEntity>> AddFruit([FromBody] FruitEntity newFruit)
        {
            try
            {
                if(newFruit is null)
                {
                    return BadRequest();
                }

                newFruit.Id = 0;

                var result = await _repository.AddFruit(newFruit);

                return CreatedAtAction(nameof(GetFruitById), new {id = newFruit.Id}, newFruit);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
