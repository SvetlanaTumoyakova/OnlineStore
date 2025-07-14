using OnlineStore.Model;

namespace OnlineStore.Data.Repositories.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByIdAsync(int id);
        Task<ProductCategory> GetByIdTrackingAsync(int id);
        Task AddAsync(ProductCategory productCategory);
        Task UpdateAsync(ProductCategory updateProductCategory);
        Task RemoveAsync(int id);
        Task<List<ProductCategory>> GetRangeAsync(int skip, int take);

    }
}
