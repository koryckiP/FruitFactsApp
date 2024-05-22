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

        public async Task<FruitEntity> DeleteFruitById(int id)
        {
            var fruit = _context.Fruits.FirstOrDefault(f => f.Id == id);
            if (fruit != null)
            {
                _context.Fruits.Remove(fruit);
                await _context.SaveChangesAsync();
                return fruit;
            }
            return null;
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

        public async Task<IEnumerable<FruitEntity>> SearchFruitByDescription(string description)
        {
            IQueryable<FruitEntity> query = _context.Fruits;

            if (description is not null)
            {
                query = query.Where(f => f.Description.Contains(description));
            }

            return await query.ToListAsync();
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
