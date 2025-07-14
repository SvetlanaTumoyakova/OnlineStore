using OnlineStore.Model;

namespace OnlineStore.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> GetByIdTrackingAsync(int id);
        Task AddAsync(Product product);
        Task Update(Product updateProduct);
        Task RemoveAsync(int id);
        Task<List<Product>> GetRange(int skip, int take);
    }
}
