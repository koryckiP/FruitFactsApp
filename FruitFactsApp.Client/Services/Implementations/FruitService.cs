using FruitFactsApp.Client.Services.Contracts;
using FruitFactsApp.Library.Models.Entities;
using System.Net.Http.Json;


namespace FruitFactsApp.Client.Services.Implementations
{
    public class FruitService : IFruitService
    {
        private readonly HttpClient _httpClient;

        public FruitService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<FruitEntity>> GetFruits()
        {
            return await _httpClient.GetFromJsonAsync<FruitEntity[]>("api/Fruits");
        }
    }
}
