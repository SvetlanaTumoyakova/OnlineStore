using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Exeptions;
using OnlineStore.Model;
using OnlineStoreClient.ViewModel;
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
        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _dbContext.Products
                                            .AsNoTracking()
                                            .ToListAsync();
            return products;
        }

        public async Task<ProductDetailsViewModel> GetByIdAsync(int id)
        {
            var productDetailsViewModel = await _dbContext.Products
                             .AsNoTracking()
                             .Include(product => product.ProductCategory)
                             .Select(product => new ProductDetailsViewModel
                             {
                                Id = product.Id,
                                Name = product.Name,
                                Description = product.Description,
                                Price = product.Price,
                                CategoryViewModel = new ProductDetailsCategoryViewModel
                                {
                                    ProductCategoryId = product.ProductCategoryId,
                                    NameProductCategory = product.ProductCategory.Name,
                                    DescriptionProductCategory = product.ProductCategory.Description
                                }
                             })
                             .FirstOrDefaultAsync(product => product.Id == id);
            return productDetailsViewModel;
        }
        public async Task<Product> GetByIdTrackingAsync(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == id) ??
             throw new NotFoundException($"Entity {nameof(ProductCategory)} not found by id {id}");

            return product;
        }

        public async Task AddAsync(Product product)
        {
            var isExist = await _dbContext.ProductCategories.AnyAsync(productCategory => productCategory.Id == product.ProductCategoryId);

            if (isExist)
            {
                throw new NotFoundException($"Entity {nameof(ProductCategory)} not found by id {id}");
            }

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

        public async Task<List<Product>> GetRangeAsync(int skip, int take)
        {
            var products = await _dbContext.Products
                                            .AsNoTracking()
                                            .Skip(skip)
                                            .Take(take)
                                            .ToListAsync();
            return products;
        }
    }
}
