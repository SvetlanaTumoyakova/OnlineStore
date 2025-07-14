using OnlineStore.Client;
using OnlineStoreClient.Managers;
using System.Net.Http.Json;

namespace OnlineStore.ConsoleClient
{
    class Program
    {
        private static ProductCategoryClient _productCategoryClient;
        private static ProductClient _productClient;

        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7180") };
            _productCategoryClient = new ProductCategoryClient(httpClient);
            _productClient = new ProductClient(httpClient);

            var categoryManager = new CategoryManager(_productCategoryClient);
            var productManager = new ProductManager(_productClient);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Работа с категориями продуктов");
                Console.WriteLine("2. Работа с продуктами");
                Console.WriteLine("3. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await categoryManager.RunAsync();
                        break;
                    case "2":
                        await productManager.RunAsync();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}