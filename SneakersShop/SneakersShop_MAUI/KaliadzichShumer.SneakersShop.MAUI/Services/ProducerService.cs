using KaliadzichShumer.SneakersShop.MAUI.Models;
using System.Net.Http.Json;
using System.Diagnostics;

namespace KaliadzichShumer.SneakersShop.MAUI.Services  {
    public class ProducerService  {
        private readonly HttpClient _httpClient;
        private readonly string _logPath;

        public ProducerService(HttpClient httpClient) {
            _httpClient = httpClient;
            _logPath = Path.Combine(FileSystem.AppDataDirectory, "api_log.txt");
            LogToFile("ProducerService initialized");
            LogToFile($"Log file path: {_logPath}");
            LogToFile($"API Base Address: {_httpClient.BaseAddress}");
        }

        private void LogToFile(string message)  {
            try  {
                var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";
                File.AppendAllText(_logPath, logMessage);
            } catch {}
        }

        public async Task<IEnumerable<Producer>> GetProducersAsync()
        {
            try  {
                LogToFile("Attempting to get producers");
                var response = await _httpClient.GetAsync("api/producers");
                LogToFile($"Get producers response: {response.StatusCode}");
                
                if (!response.IsSuccessStatusCode) {
                    var error = await response.Content.ReadAsStringAsync();
                    LogToFile($"Error getting producers: {response.StatusCode} - {error}");
                    return Enumerable.Empty<Producer>();
                }
                
                var producers = await response.Content.ReadFromJsonAsync<IEnumerable<Producer>>();
                LogToFile($"Successfully retrieved {producers?.Count() ?? 0} producers");
                return producers ?? Enumerable.Empty<Producer>();
            }
            catch (Exception ex){
                LogToFile($"Exception in GetProducersAsync: {ex}");
                return Enumerable.Empty<Producer>();
            }
        }

        public async Task<Producer?> GetProducerByIdAsync(int id) {
            try {
                var response = await _httpClient.GetAsync($"api/producers/{id}");
                if (!response.IsSuccessStatusCode) {
                    Debug.WriteLine($"Error getting producer {id}: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<Producer>();
            } catch (Exception ex) {
                Debug.WriteLine($"Exception in GetProducerByIdAsync: {ex}");
                return null;
            }
        }

        public async Task<bool> AddProducerAsync(Producer producer)  {
            try {
                LogToFile($"Attempting to add producer with name: {producer.Name}");
                LogToFile($"API URL: {_httpClient.BaseAddress}api/producers");
                
                producer.Products ??= new List<Product>();
                
                LogToFile($"Producer object: {{ Id: {producer.Id}, Name: '{producer.Name}', Products: {producer.Products.Count} items }}");

                var jsonContent = System.Text.Json.JsonSerializer.Serialize(producer);
                LogToFile($"Request JSON: {jsonContent}");
                
                var response = await _httpClient.PostAsJsonAsync("api/producers", producer);
                var responseContent = await response.Content.ReadAsStringAsync();
                LogToFile($"Response status code: {response.StatusCode}");
                LogToFile($"Response content: {responseContent}");
                
                if (!response.IsSuccessStatusCode) {
                    LogToFile($"Error adding producer: {response.StatusCode} - {responseContent}");
                    return false;
                }
                LogToFile("Successfully added producer");
                return true;
            } catch (Exception ex) {
                LogToFile($"Exception in AddProducerAsync: {ex}");
                LogToFile($"Exception details: {ex.Message}");
                if (ex.InnerException != null) {
                    LogToFile($"Inner exception: {ex.InnerException.Message}");
                    LogToFile($"Inner exception stack trace: {ex.InnerException.StackTrace}");
                }
                return false;
            }
        }

        public async Task<bool> UpdateProducerAsync(Producer producer)  {
            try  {
                var response = await _httpClient.PutAsJsonAsync($"api/producers/{producer.Id}", producer);
                if (!response.IsSuccessStatusCode) {
                    Debug.WriteLine($"Error updating producer: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return false;
                }
                return true;
            } catch (Exception ex)  {
                Debug.WriteLine($"Exception in UpdateProducerAsync: {ex}");
                return false;
            }
        }

        public async Task<bool> DeleteProducerAsync(int id) {
            try  {
                var response = await _httpClient.DeleteAsync($"api/producers/{id}");
                if (!response.IsSuccessStatusCode) {
                    Debug.WriteLine($"Error deleting producer: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return false;
                }
                return true;
            } catch (Exception ex)  {
                Debug.WriteLine($"Exception in DeleteProducerAsync: {ex}");
                return false;
            }
        }
    }
}
