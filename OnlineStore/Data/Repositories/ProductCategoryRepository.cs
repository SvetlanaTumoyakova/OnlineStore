using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Exeptions;
using OnlineStore.Model;

namespace OnlineStore.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private OnlineStoreDBContext _dbContext;
        public ProductCategoryRepository(OnlineStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ProductCategory>> GetAllAsync()
        {
            var productCategories = await _dbContext.ProductCategories
                                                     .AsNoTracking()
                                                     .ToListAsync();
            return productCategories;
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            var productCategories = await _dbContext.ProductCategories
                             .AsNoTracking()
                             .FirstOrDefaultAsync(productCategories => productCategories.Id == id);
            return productCategories;
        }
        public async Task<ProductCategory> GetByIdTrackingAsync(int id)
        {
            var productCategory = await _dbContext.ProductCategories.FirstOrDefaultAsync(productCategory => productCategory.Id == id)??
             throw new NotFoundException($"Entity {nameof(ProductCategory)} not found by id {id}");

            return productCategory;
        }

        public async Task AddAsync(ProductCategory productCategory)
        {
            await _dbContext.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProductCategory updateProductCategory)
        {
            var productCategory = await GetByIdTrackingAsync(updateProductCategory.Id);
            productCategory.Name = updateProductCategory.Name;
            productCategory.Description = updateProductCategory.Description;

            _dbContext.Update(productCategory);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(int id)
        {
            var productCategory = await GetByIdTrackingAsync(id);

            _dbContext.ProductCategories.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductCategory>> GetRangeAsync(int skip, int take)
        {
            var productCategories = await _dbContext.ProductCategories
                                                        .AsNoTracking()
                                                        .Skip(skip)
                                                        .Take(take)
                                                        .ToListAsync();
            return productCategories;
        }
    }
}
