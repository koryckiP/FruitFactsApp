﻿using FruitFactsApp.Library.Models.Entities;

namespace FruitFactsApp.Server.Repositories.Contracts
{
    public interface IFruitsRepository
    {
        Task<IEnumerable<FruitEntity>> GetAllFruits();
        Task<FruitEntity> GetFruitById(int id);
        Task<FruitEntity> GetFruitByName(string name);
        Task<FruitEntity> AddFruit(FruitEntity fruit);
    }
}
