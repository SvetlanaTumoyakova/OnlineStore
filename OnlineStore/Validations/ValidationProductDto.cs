using OnlineStore.Dto;

namespace OnlineStore.Validations;

public class ValidationProductDto
{
    public static ValidateResult Validate(ProductDto productDto)
    {
        if (string.IsNullOrWhiteSpace(productDto.Name))
        {
            return new ValidateResult
            {
                IsValid = false,
                Error = "Ошибка"
            };
        }
        if (string.IsNullOrEmpty(productDto.Name))
        {
            return new ValidateResult
            {
                IsValid = false,
                Error = "Ошибка"
            };
        }
        if (productDto.Name.Length > 250)
        {
            return new ValidateResult
            {
                IsValid = false,
                Error = "Ошибка"
            };
        }

        if (string.IsNullOrWhiteSpace(productDto.Description) || string.IsNullOrEmpty(productDto.Description) || productDto.Name.Length > 2000)
        {
            return new ValidateResult
            {
                IsValid = false,
                Error = "Ошибка"
            };
        }

        if (productDto.Price <= 0)
        {
            return new ValidateResult
            {
                IsValid = false,
                Error = "Ошибка"
            };
        }

        return new ValidateResult
        {
            IsValid = true,
            Error = string.Empty
        };
    }

    public class ValidateResult
    {
        public required bool IsValid { get; init; }
        public required string Error { get; init; }
    }
}
