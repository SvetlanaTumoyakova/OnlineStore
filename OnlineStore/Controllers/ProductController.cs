using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Exeptions;
using OnlineStore.Model;

namespace OnlineStore.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var productDetailsVM = await _productRepository.GetByIdAsync(id);

        return Ok(productDetailsVM);
    }

    [HttpGet("{skip}/{take}")]
    public async Task<ActionResult> GetRangeAsync(int skip, int take)
    {
        var products = await _productRepository.GetRangeAsync(skip, take);

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(ProductDto productDto)
    {
        var validateResult = ValidationProductDto.Validate(productDto);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult);
        }

        try
        {
            var product = MapperProductDto.Map(productDto);
            await _productRepository.AddAsync(product);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            await _productRepository.RemoveAsync(id);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(Product product)
    {
        try
        {
            await _productRepository.UpdateAsync(product);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return NoContent();
    }
}