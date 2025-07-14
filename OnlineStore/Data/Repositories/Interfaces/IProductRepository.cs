using OnlineStore.Model;

namespace OnlineStore.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetByIdTrackingAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product updateProduct);
        Task RemoveAsync(int id);
        Task<List<Product>> GetRangeAsync(int skip, int take);
    }
}
