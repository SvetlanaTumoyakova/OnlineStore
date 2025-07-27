using FluentValidation;
using OnlineStoreClient.Dto;

namespace OnlineStore.Validations;

public class ProductValidation : AbstractValidator<ProductDto>
{
    public const int MaxNameLength = 1024;
    public const int MaxDescriptionLength = 2048;

    public ProductValidation()
    {
        RuleFor(createProductCommand =>
                       createProductCommand.Name)
                           .NotEmpty()
                           .WithMessage("Название товара обязательно для заполнения.")
                           .MaximumLength(MaxNameLength)
                           .WithMessage($"Название товара не должно превышать {MaxNameLength} символов.");

        RuleFor(createProductCommand =>
                        createProductCommand.Description)
                            .NotEmpty()
                            .WithMessage("Описание товара обязательно для заполнения.")
                            .MaximumLength(MaxDescriptionLength)
                            .WithMessage($"Описание товара не должно превышать {MaxDescriptionLength} символов.");

        RuleFor(createProductCommand =>
                        createProductCommand.Price)
                            .GreaterThan(0)
                            .WithMessage("Цена товара должна быть больше нуля.");
    }
}
