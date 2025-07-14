using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Client
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product[]> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<Product[]>("api/products");
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
        }

        public async Task<Product[]> GetRangeAsync(int skip, int take)
        {
            return await _httpClient.GetFromJsonAsync<Product[]>($"api/products/{skip}/{take}");
        }

        public async Task AddAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync("api/products", product);
            response.EnsureSuccessStatusCode();
        }
        public async Task RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
