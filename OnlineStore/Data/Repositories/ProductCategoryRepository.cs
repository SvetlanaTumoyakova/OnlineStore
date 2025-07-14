using Microsoft.EntityFrameworkCore;
using OnlineStore.Model;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repositories
{
    public class ProductCategoryRepository
    {
        private OnlineStoreDBContext _dbContext;
        public ProductCategoryRepository(OnlineStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private Task<List<ProductCategory>> GetAll()
        {
            var productCategories = _dbContext.ProductCategories
                                                     .AsNoTracking()
                                                     .ToListAsync();
            return productCategories;
        }

        private Task<ProductCategory> GetById(int id)
        {
            var productCategories = _dbContext.ProductCategories
                             .AsNoTracking()
                             .FirstOrDefaultAsync(productCategories => productCategories.Id == id);
            return productCategories;
        }
        private async Task<ProductCategory> GetByIdTrackingAsync(int id)
        {
            var productCategory = await _dbContext.ProductCategories.FirstOrDefaultAsync(productCategory => productCategory.Id == id);
            return productCategory;
        }
        private Task<List<ProductCategory>> GetRange(int skip, int take)
        {
            var productCategories = _dbContext.ProductCategories
                                                        .AsNoTracking()
                                                        .Skip(skip)
                                                        .Take(take)
                                                        .ToListAsync();
            return productCategories;
        }
        public async Task AddAsync(ProductCategory productCategory)
        {
            await _dbContext.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var productCategory = await GetByIdTrackingAsync(id);
            _dbContext.ProductCategories.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(ProductCategory updateProductCategory)
        {
            var productCategory = await GetByIdTrackingAsync(updateProductCategory.Id);
            _dbContext.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}
