using OnlineStore.Validations;
using OnlineStoreClient.Dto;
using OnlineStoreClient.Model;
using OnlineStoreClient.ViewModel;
using System.Net;
using System.Net.Http.Json;

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

        public async Task AddAsync(ProductDto productDto)
        {
            var validator = new ProductValidation();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                Console.WriteLine($"Ошибки валидации: {string.Join(", ", errors)}");
                return;
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/products", productDto);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                    Console.WriteLine($"Ошибка валидации от сервера: {string.Join(", ", errorMessages)}");
                }

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при добавлении товара: {ex.Message}");
            }
        }

        public async Task UpdateAsync(int id, ProductDto productDto)
        {
            var validator = new ProductValidation();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                Console.WriteLine($"Ошибки валидации: {string.Join(", ", errors)}");
                return;
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", productDto);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                    Console.WriteLine($"Ошибка валидации от сервера: {string.Join(", ", errorMessages)}");
                    return;
                }

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Ошибка сети при обновлении данных о товаре.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при обновлении данных о товаре: {ex.Message}");
            }
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/products/{id}");

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                    Console.WriteLine($"Ошибка при удалении товара: {string.Join(", ", errorMessages)}");
                    return;
                }

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении товара.", ex);
            }
        }
    }
}
