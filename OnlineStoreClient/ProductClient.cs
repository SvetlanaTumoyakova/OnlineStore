using OnlineStoreClient.Dto;
using OnlineStoreClient.Model;
using OnlineStoreClient.Validations;
using OnlineStoreClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreClient
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

        public async Task<ProductDetailsViewModel> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDetailsViewModel>($"api/products/{id}");
        }

        public async Task<Product[]> GetRangeAsync(int skip, int take)
        {
            return await _httpClient.GetFromJsonAsync<Product[]>($"api/products/{skip}/{take}");
        }

        public async Task AddAsync(ProductDto ProductDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", ProductDto);
            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                var productDetailsVM = await response.Content.ReadFromJsonAsync<ValidateResult>();
            }
        }

        public async Task UpdateAsync(ProductDto ProductDto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/products", ProductDto);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var productDetailsVM = await response.Content.ReadFromJsonAsync<ValidateResult>();
            }
        }
        public async Task RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var productDetailsVM = await response.Content.ReadFromJsonAsync<ValidateResult>();
            }
        }
    }
}
