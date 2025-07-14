using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Model;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private OnlineStoreDBContext _dbContext;
        public ProductRepository(OnlineStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Product>> GetAllAsync()
        {
            var products = _dbContext.Products
                                            .AsNoTracking()
                                            .ToListAsync();
            return products;
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _dbContext.Products
                             .AsNoTracking()
                             .FirstOrDefaultAsync(product => product.Id == id);
            return product;
        }
        public async Task<Product> GetByIdTrackingAsync(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
            return product;
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product updateProduct)
        {
            var product = await GetByIdTrackingAsync(updateProduct.Id);

            product.Name = updateProduct.Name;
            product.Description = updateProduct.Description;
            product.Price = updateProduct.Price;
            product.ProductCategoryId = updateProduct.ProductCategoryId;

            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(int id)
        {
            var product = await GetByIdTrackingAsync(id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Product>> GetRangeAsync(int skip, int take)
        {
            var products = _dbContext.Products
                                            .AsNoTracking()
                                            .Skip(skip)
                                            .Take(take)
                                            .ToListAsync();
            return products;
        }
    }
}
