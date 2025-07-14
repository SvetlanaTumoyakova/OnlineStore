using OnlineStore.Client;
using OnlineStore.Model;
using OnlineStoreClient.Dto;

namespace OnlineStoreClient.Managers
{
    public class ProductManager
    {
        private readonly ProductClient _productClient;
        private readonly ProductCategoryClient _productCategoryClient;
        public ProductManager(ProductClient productClient, ProductCategoryClient productCategoryClient)
        {
            _productClient = productClient;
            _productCategoryClient = productCategoryClient;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие с продуктами:");
                Console.WriteLine("1. Получить все продукты");
                Console.WriteLine("2. Добавить продукт");
                Console.WriteLine("3. Обновить продукт");
                Console.WriteLine("4. Удалить продукт");
                Console.WriteLine("5. Назад");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GetAllProductsAsync();
                        break;
                    case "2":
                        await AddProductAsync();
                        break;
                    case "3":
                        await UpdateProductAsync();
                        break;
                    case "4":
                        await RemoveProductAsync();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        private async Task GetAllProductsAsync()
        {
            var products = await _productClient.GetAllAsync();
            Console.WriteLine("Все продукты:");
            foreach (var product in products)
            {
                Console.WriteLine($"- ID: {product.Id}, Название: {product.Name}, Цена: {product.Price}");
            }
        }

        private async Task AddProductAsync()
        {
            var categories = await _productCategoryClient.GetAllAsync();
            Console.WriteLine("Доступные категории:");
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name} (ID: {categories[i].Id})");
            }
            Console.Write("Выберите категорию (введите номер): ");
            var categoryChoiceInput = Console.ReadLine();
            if (int.TryParse(categoryChoiceInput, out int categoryChoice) &&
                    categoryChoice > 0 && categoryChoice <= categories.Length)
            {
                var selectedCategory = categories[categoryChoice - 1];

                Console.Write("Введите название нового продукта: ");
                var name = Console.ReadLine();

                Console.Write("Введите описание нового продукта: ");
                var description = Console.ReadLine();

                Console.Write("Введите цену нового продукта: ");
                var priceInput = Console.ReadLine();

                if (decimal.TryParse(priceInput, out decimal price))
                {
                    var newProductDto = new ProductDto
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        ProductCategoryId = selectedCategory.Id,
                    };

                    await _productClient.AddAsync(newProductDto);
                    Console.WriteLine("Продукт добавлен.");
                }
                else
                {
                    Console.WriteLine("Введенная цена некорректна. Пожалуйста, убедитесь," +
                        "что вы вводите число в правильном формате (например, 123.45) и попробуйте снова.");
                }
            }
            else
            {
                Console.WriteLine("Неверный выбор. Такой категории не существует.");
            }
        }

        private async Task UpdateProductAsync()
        {
            Console.Write("Введите ID продукта для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var product = await _productClient.GetByIdAsync(id);
                if (product != null)
                {
                    Console.Write("Введите новое название продукта: ");
                    product.Name = Console.ReadLine();
                    Console.Write("Введите новую цену продукта: ");
                    var priceInput = Console.ReadLine();

                    if (decimal.TryParse(priceInput, out decimal price))
                    {
                        product.Price = price;
                        await _productClient.UpdateAsync(product);
                        Console.WriteLine("Продукт обновлен.");
                    }
                    else
                    {
                        Console.WriteLine("Неверная цена.");
                    }
                }
                else
                {
                    Console.WriteLine("Продукт не найден.");
                }
            }
            else
            {
                Console.WriteLine("Неверный ID.");
            }
        }

        private async Task RemoveProductAsync()
        {
            Console.Write("Введите ID продукта для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _productClient.RemoveAsync(id);
                Console.WriteLine("Продукт удален.");
            }
            else
            {
                Console.WriteLine("Неверный ID.");
            }
        }
    }
}
