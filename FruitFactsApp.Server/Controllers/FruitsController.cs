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

                var fruitName = await _repository.GetFruitByName(newFruit.Name);
                if (fruitName != null)
                {
                    ModelState.AddModelError("name", $"Fruit of given name already exists in database: {fruitName}");
                    return BadRequest(ModelState);
                }

                var result = await _repository.AddFruit(newFruit);

                return CreatedAtAction(nameof(GetFruitById), new {id = newFruit.Id}, newFruit);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FruitEntity>> UpdateFruit(int id, FruitEntity newFruit)
        {
            try
            {
                if(id != newFruit.Id)
                {
                    return BadRequest("Fruit Id mismatch");
                }

                var fruitToUpdate = await _repository.GetFruitById(id);

                if (fruitToUpdate is null)
                {
                    return NotFound("No Fruit of specified Id was found in database");
                }

                var allFruits = await _repository.GetAllFruits();
                foreach( var f in  allFruits)
                {
                    if(f.Name.Equals(newFruit.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ModelState.AddModelError("name", "Updating would cause a duplicate name");
                        return BadRequest(ModelState);
                    }
                }

                return await _repository.UpdateFruit(newFruit);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while updating data:: "+ex.Message);
            }
        }
    }
}
