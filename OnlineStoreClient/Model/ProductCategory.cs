using OnlineStore.Model;

namespace OnlineStoreClient.Model
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        List<Product>? Products { get; set; }
    }
}
