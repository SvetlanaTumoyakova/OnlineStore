using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Repositories;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Dto;
using OnlineStore.Exeptions;
using OnlineStore.Mappers;
using OnlineStore.Model;
using OnlineStore.Validations;

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
        var productDetailsViewModel = await _productRepository.GetByIdAsync(id);

        return Ok(productDetailsViewModel);
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

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateAsync(int id, ProductDto productDto)
    {
        var validateResult = ValidationProductDto.Validate(productDto);
        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult);
        }

        try
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Продукт не найден.");
            }

            var product = MapperProductDto.Map(productDto);
            product.Id = existingProduct.Id; 

            await _productRepository.UpdateAsync(product);
            return Ok(product);
        }
        catch (NotFoundException)
        {
            return NotFound("Продукт не найден.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при обновлении продукта: {ex.Message}");
        }
    }
}