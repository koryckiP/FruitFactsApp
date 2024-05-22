using FruitFactsApp.Library.Models.Entities;

namespace FruitFactsApp.Server.Repositories.Contracts
{
    public interface IFruitsRepository
    {
        Task<IEnumerable<FruitEntity>> GetAllFruits();
        Task<FruitEntity> GetFruitById(int id);
        Task<FruitEntity> GetFruitByName(string name);
        Task<FruitEntity> AddFruit(FruitEntity fruit);
        Task<FruitEntity> UpdateFruit(FruitEntity newFruit);
        Task<FruitEntity> DeleteFruitById(int id);
        Task<IEnumerable<FruitEntity>> SearchFruitByDescription(string description);
    }
}
