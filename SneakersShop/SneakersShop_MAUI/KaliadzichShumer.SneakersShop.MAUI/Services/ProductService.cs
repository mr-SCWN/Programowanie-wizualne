using KaliadzichShumer.SneakersShop.MAUI.Models;
using System.Net.Http.Json;
using System.Diagnostics;

namespace KaliadzichShumer.SneakersShop.MAUI.Services {
    public class ProductService  {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)  {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() {
            try  {
                var response = await _httpClient.GetAsync("api/products");
                if (!response.IsSuccessStatusCode) {
                    Debug.WriteLine($"Error getting products: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return Enumerable.Empty<Product>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>() 
                       ?? Enumerable.Empty<Product>();
            } catch (Exception ex)  {
                Debug.WriteLine($"Exception in GetProductsAsync: {ex}");
                return Enumerable.Empty<Product>();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id) {
            try {
                var response = await _httpClient.GetAsync($"api/products/{id}");
                
                if (!response.IsSuccessStatusCode) {
                    Debug.WriteLine($"Error getting product {id}: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<Product>();
            } catch (Exception ex) {
                Debug.WriteLine($"Exception in GetProductByIdAsync: {ex}");
                return null;
            }
        }

        public async Task<bool> AddProductAsync(Product product) {
            try {
                var response = await _httpClient.PostAsJsonAsync("api/products", product);
                if (!response.IsSuccessStatusCode) {
                    Debug.WriteLine($"Error adding product: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return false;
                }
                return true;
            } catch (Exception ex) {
                Debug.WriteLine($"Exception in AddProductAsync: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product) {
            try{
                var response = await _httpClient.PutAsJsonAsync($"api/products/{product.Id}", product);
                if (!response.IsSuccessStatusCode) {
                    Debug.WriteLine($"Error updating product: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return false;
                }
                return true;
            } catch (Exception ex)  {
                Debug.WriteLine($"Exception in UpdateProductAsync: {ex}");
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)  {
            try  {
                var response = await _httpClient.DeleteAsync($"api/products/{id}");
                if (!response.IsSuccessStatusCode) {

                    Debug.WriteLine($"Error deleting product: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return false;
                }
                return true;
            }  catch (Exception ex) {
                Debug.WriteLine($"Exception in DeleteProductAsync: {ex}");
                return false;
            }
        }
    }
}
