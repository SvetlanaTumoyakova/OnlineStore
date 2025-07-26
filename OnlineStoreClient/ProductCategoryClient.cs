using FluentValidation;
using OnlineStore.Validations;
using OnlineStoreClient.Dto;
using OnlineStoreClient.Model;
using System.Net;
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
            var validator = new ProductCategoryValidation();
            var validationResult = await validator.ValidateAsync(productCategory);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                Console.WriteLine($"Ошибки валидации: {string.Join(", ", errors)}");
                return;
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ProductCategory", productCategory);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                    Console.WriteLine($"Ошибка при добавлении категории товара: {string.Join(", ", errorMessages)}");
                    return;
                }

                response.EnsureSuccessStatusCode();
                Console.WriteLine("Категория товара успешно добавлена.");
        }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при добавлении категории товара: {ex.Message}");
            }
        }

        public async Task UpdateAsync( ProductCategory productCategory)
        {
            var validator = new ProductCategoryValidation();
            var validationResult = await validator.ValidateAsync(productCategory);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                Console.WriteLine($"Ошибки валидации: {string.Join(", ", errors)}");
                return;
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/ProductCategory", productCategory);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                    Console.WriteLine($"Ошибка при обновлении категории товара: {string.Join(", ", errorMessages)}");
                    return;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Категория товара не найдена для обновления.");
                    return;
                }

                response.EnsureSuccessStatusCode();
                Console.WriteLine("Категория товара успешно обновлена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при обновлении категории товара: {ex.Message}");
            }
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ProductCategory/{id}");

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                    Console.WriteLine($"Ошибка при удалении категории товара: {string.Join(", ", errorMessages)}");
                    return;
                }

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении категории товара.", ex);
            }
        }
    }
}
