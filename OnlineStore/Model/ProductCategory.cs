namespace OnlineStore.Model
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        List<Product>? Products { get; set; }
    }
}
