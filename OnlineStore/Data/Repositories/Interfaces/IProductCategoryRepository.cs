using OnlineStore.Model;

namespace OnlineStore.Data.Repositories.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAll();
        Task<ProductCategory> GetById(int id);
        Task<ProductCategory> GetByIdTrackingAsync(int id);
        Task AddAsync(ProductCategory productCategory);
        Task Update(ProductCategory updateProductCategory);
        Task RemoveAsync(int id);
        Task<List<ProductCategory>> GetRange(int skip, int take);

    }
}
