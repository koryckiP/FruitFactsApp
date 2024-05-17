using FruitFactsApp.Library.Models.Entities;
using FruitFactsApp.Server.Data;
using FruitFactsApp.Server.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FruitFactsApp.Server.Repositories.Implementations
{
    public class FruitsRepository : IFruitsRepository
    {
        private readonly AppDbContext _context;

        public FruitsRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<FruitEntity> AddFruit(FruitEntity fruit)
        {
            var result = await _context.Fruits.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public  async Task<IEnumerable<FruitEntity>> GetAllFruits()
        {
            return await _context.Fruits.ToListAsync();
        }

        //possible null reference is intentional here and is checked in controller
        public async Task<FruitEntity> GetFruitById(int id)
        {
            return await _context.Fruits.FirstOrDefaultAsync(f => f.Id == id);
        }

        //possible null reference is intentional here and is checked in controller
        public async Task<FruitEntity> GetFruitByName(string name)
        {
            return await _context.Fruits.FirstOrDefaultAsync(f => f.Name == name);
        }

        public async Task<FruitEntity> UpdateFruit(FruitEntity newFruit)
        {
            //null check done in controller, before this executes
            var result = await _context.Fruits.FirstOrDefaultAsync(f => f.Id ==  newFruit.Id);

            if (result != null)
            {
                result.Name = newFruit.Name;
                result.Description = newFruit.Description;
                result.Type = newFruit.Type;


                await _context.SaveChangesAsync();

                return result;
            }
            return null;
        }
    }
}
