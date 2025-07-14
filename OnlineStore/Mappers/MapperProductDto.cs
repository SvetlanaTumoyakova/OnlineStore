using OnlineStore.Dto;
using OnlineStore.Model;

namespace OnlineStore.Mappers;

public class MapperProductDto
{
    public static Product Map(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            ProductCategoryId = productDto.ProductCategoryId,
        };

        return product;
    }
}