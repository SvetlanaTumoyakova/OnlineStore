using OnlineStoreClient.Model;
using System.Net.Http.Json;

namespace OnlineStoreClient
{
    public class ProductCategoryClient
    {
        private readonly HttpClient _httpClient;

        public ProductCategoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductCategory[]> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<ProductCategory[]>("api/ProductCategory");
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductCategory>($"api/ProductCategory/{id}");
        }

        public async Task<ProductCategory[]> GetRangeAsync(int skip, int take)
        {
            return await _httpClient.GetFromJsonAsync<ProductCategory[]>($"api/ProductCategory/{skip}/{take}");
        }

        public async Task AddAsync(ProductCategory productCategory)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ProductCategory", productCategory);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateAsync(ProductCategory productCategory)
        {
            var response = await _httpClient.PutAsJsonAsync("api/ProductCategory", productCategory);
            response.EnsureSuccessStatusCode();
        }
        public async Task RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ProductCategory/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
