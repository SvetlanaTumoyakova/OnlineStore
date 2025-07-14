using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Exeptions;
using OnlineStore.Model;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : Controller
    {
        private IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var productCategories = await _productCategoryRepository.GetAllAsync();

            return Ok(productCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);

            return Ok(productCategory);
        }

        [HttpGet("{skip}/{take}")]
        public async Task<ActionResult> GetRangeAsync(int skip, int take)
        {
            var productCategories = await _productCategoryRepository.GetRangeAsync(skip, take);

            return Ok(productCategories);
        }
        [HttpPost]
        public async Task<ActionResult> AddAsync(ProductCategory product)
        {
           await _productCategoryRepository.AddAsync(product);
           return Created();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(ProductCategory productCategory)
        {
            try
            {
                await _productCategoryRepository.UpdateAsync(productCategory);
            }
            catch (NotFoundException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAsync(int id)
        {
            try
            {
                await _productCategoryRepository.RemoveAsync(id);
            }
            catch (NotFoundException)
            {
                throw;
            }
            
            return NoContent();
        }
    }
}
