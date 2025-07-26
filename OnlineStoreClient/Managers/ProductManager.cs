using OnlineStoreClient.Dto;
using OnlineStoreClient.ViewModel;

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
                Console.WriteLine("2. Получить продукт по ID");
                Console.WriteLine("3. Добавить продукт");
                Console.WriteLine("4. Обновить продукт");
                Console.WriteLine("5. Удалить продукт");
                Console.WriteLine("6. Назад");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GetAllProductsAsync();
                        break;
                    case "2":
                        await GetProductByIdAsync();
                        break;
                    case "3":
                        await AddProductAsync();
                        break;
                    case "4":
                        await UpdateProductAsync();
                        break;
                    case "5":
                        await RemoveProductAsync();
                        break;
                    case "6":
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
        private async Task GetProductByIdAsync()
        {
            Console.Write("Введите ID продукта: ");
            if (int.TryParse(Console.ReadLine(), out int id)) // Читаем ID от пользователя
            {
                try
                {
                    ProductDetailsViewModel productDetails = await _productClient.GetByIdAsync(id);

                    if (productDetails != null)
                    {
                        Console.WriteLine($"Название: {productDetails.Name}");
                        Console.WriteLine($"Описание: {productDetails.Description}");
                        Console.WriteLine($"Цена: {productDetails.Price} руб."); 

                        if (productDetails.CategoryViewModel != null)
                        {
                            Console.WriteLine($"ID категории: {productDetails.CategoryViewModel.ProductCategoryId}");
                            Console.WriteLine($"Название категории: {productDetails.CategoryViewModel.NameProductCategory}");
                            Console.WriteLine($"Описание категории: {productDetails.CategoryViewModel.DescriptionProductCategory}");
                        }
                        else
                        {
                            Console.WriteLine("Информация о категории недоступна.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Продукт не найден.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении продукта: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID. Пожалуйста, введите числовое значение.");
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
                var productDetails = await _productClient.GetByIdAsync(id);
                if (productDetails != null)
                {
                    var updatedProductDto = new ProductDto
                    {
                        Name = productDetails.Name,
                        Description = productDetails.Description,
                        Price = productDetails.Price,
                        ProductCategoryId = productDetails.CategoryViewModel.ProductCategoryId
                    };

                    Console.Write("Введите новое название продукта (оставьте пустым, чтобы не изменять): ");
                    var newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        updatedProductDto.Name = newName;
                    }

                    Console.Write("Введите новую цену продукта (оставьте пустым, чтобы не изменять): ");
                    var priceInput = Console.ReadLine();
                    if (decimal.TryParse(priceInput, out decimal newPrice))
                    {
                        updatedProductDto.Price = newPrice;
                    }
                    await _productClient.UpdateAsync(productDetails.Id, updatedProductDto);
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
            }
            else
            {
                Console.WriteLine("Неверный ID.");
            }
        }
    }
}
