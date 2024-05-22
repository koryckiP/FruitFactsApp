using FruitFactsApp.Library.Models.Entities;

namespace FruitFactsApp.Client.Services.Contracts
{
    public interface IFruitService
    {
        Task<IEnumerable<FruitEntity>> GetFruits();
    }
}
