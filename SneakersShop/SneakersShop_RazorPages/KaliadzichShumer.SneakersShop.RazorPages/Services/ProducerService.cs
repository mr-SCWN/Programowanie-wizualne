using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KaliadzichShumer.SneakersShop.RazorPages.Models;

namespace KaliadzichShumer.SneakersShop.RazorPages.Services
{
    public class ProducerService
    {
        private readonly HttpClient _httpClient;

        public ProducerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Producer>> GetProducersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Producer>>("api/producers");
        }

        public async Task<Producer> GetProducerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Producer>($"api/producers/{id}");
        }

        public async Task AddProducerAsync(Producer producer)
        {
            await _httpClient.PostAsJsonAsync("api/producers", producer);
        }

        public async Task UpdateProducerAsync(Producer producer)
        {
            await _httpClient.PutAsJsonAsync($"api/producers/{producer.Id}", producer);
        }

        public async Task DeleteProducerAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/producers/{id}");
        }
    }
}
