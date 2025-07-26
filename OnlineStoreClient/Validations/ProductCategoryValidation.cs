using FluentValidation;
using OnlineStoreClient.Dto;
using OnlineStoreClient.Model;

namespace OnlineStore.Validations;

public class ProductCategoryValidation : AbstractValidator<ProductCategory>
{
    public const int MaxNameLength = 10;
    public const int MaxDescriptionLength = 1024;

    public ProductCategoryValidation()
    {
        RuleFor(createProductCategoryCommand =>
                       createProductCategoryCommand.Name)
                           .NotEmpty()
                           .WithMessage("Название товара обязательно для заполнения.")
                           .MaximumLength(MaxNameLength)
                           .WithMessage($"Название товара не должно превышать {MaxNameLength} символов.");

    }
}
