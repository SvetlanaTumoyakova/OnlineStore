using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Model;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private OnlineStoreDBContext _dbContext;
        public ProductCategoryRepository(OnlineStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<ProductCategory>> GetAll()
        {
            var productCategories = _dbContext.ProductCategories
                                                     .AsNoTracking()
                                                     .ToListAsync();
            return productCategories;
        }

        public Task<ProductCategory> GetById(int id)
        {
            var productCategories = _dbContext.ProductCategories
                             .AsNoTracking()
                             .FirstOrDefaultAsync(productCategories => productCategories.Id == id);
            return productCategories;
        }
        public async Task<ProductCategory> GetByIdTrackingAsync(int id)
        {
            var productCategory = await _dbContext.ProductCategories.FirstOrDefaultAsync(productCategory => productCategory.Id == id);
            return productCategory;
        }

        public async Task AddAsync(ProductCategory productCategory)
        {
            await _dbContext.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(ProductCategory updateProductCategory)
        {
            var productCategory = await GetByIdTrackingAsync(updateProductCategory.Id);
            _dbContext.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(int id)
        {
            var productCategory = await GetByIdTrackingAsync(id);
            _dbContext.ProductCategories.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<ProductCategory>> GetRange(int skip, int take)
        {
            var productCategories = _dbContext.ProductCategories
                                                        .AsNoTracking()
                                                        .Skip(skip)
                                                        .Take(take)
                                                        .ToListAsync();
            return productCategories;
        }
    }
}
