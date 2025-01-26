using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KaliadzichShumer.SneakersShop.RazorPages.Models;

namespace KaliadzichShumer.SneakersShop.RazorPages.Services {
    public class ProductService  {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(){
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("api/products");
        }

        public async Task<Product> GetProductByIdAsync(int id) {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
        }

        public async Task AddProductAsync(Product product) {
            await _httpClient.PostAsJsonAsync("api/products", product);
        }

        public async Task UpdateProductAsync(Product product){
            await _httpClient.PutAsJsonAsync($"api/products/{product.Id}", product);
        }

        public async Task DeleteProductAsync(int id) {
            await _httpClient.DeleteAsync($"api/products/{id}");
        }
    }
}
