using OnlineStoreClient.Model;

namespace OnlineStoreClient.Managers
{
    public class CategoryManager
    {
        private readonly ProductCategoryClient _productCategoryClient;

        public CategoryManager(ProductCategoryClient productCategoryClient)
        {
            _productCategoryClient = productCategoryClient;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие с категориями:");
                Console.WriteLine("1. Получить все категории");
                Console.WriteLine("2. Добавить категорию");
                Console.WriteLine("3. Обновить категорию");
                Console.WriteLine("4. Удалить категорию");
                Console.WriteLine("5. Назад");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GetAllCategoriesAsync();
                        break;
                    case "2":
                        await AddCategoryAsync();
                        break;
                    case "3":
                        await UpdateCategoryAsync();
                        break;
                    case "4":
                        await RemoveCategoryAsync();
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

        private async Task GetAllCategoriesAsync()
        {
            var categories = await _productCategoryClient.GetAllAsync();
            Console.WriteLine("Все категории продуктов:");
            foreach (var category in categories)
            {
                Console.WriteLine($"- ID: {category.Id}, Название: {category.Name}");
            }
        }

        private async Task AddCategoryAsync()
        {
            Console.Write("Введите название новой категории: ");
            var name = Console.ReadLine();
            var newCategory = new ProductCategory { Name = name };
            await _productCategoryClient.AddAsync(newCategory);
            Console.WriteLine("Категория добавлена.");
        }

        private async Task UpdateCategoryAsync()
        {
            Console.Write("Введите ID категории для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var category = await _productCategoryClient.GetAllAsync();
                var existingCategory = Array.Find(category, c => c.Id == id);
                if (existingCategory != null)
                {
                    Console.Write("Введите новое название категории: ");
                    existingCategory.Name = Console.ReadLine();
                    await _productCategoryClient.UpdateAsync(existingCategory);
                    Console.WriteLine("Категория обновлена.");
                }
                else
                {
                    Console.WriteLine("Категория не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Неверный ID.");
            }
        }

        private async Task RemoveCategoryAsync()
        {
            Console.Write("Введите ID категории для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _productCategoryClient.RemoveAsync(id);
                Console.WriteLine("Категория удалена.");
            }
            else
            {
                Console.WriteLine("Неверный ID.");
            }
        }
    }
}
