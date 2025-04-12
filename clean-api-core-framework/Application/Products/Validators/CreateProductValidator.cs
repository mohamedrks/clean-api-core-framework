using Application.Products.Commands;
using FluentValidation;

namespace Application.Products.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Only 500 Characters are allowed for description");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be positive");

            RuleFor(x => x.Image)
                .Must(image => image == null || image.Length > 0)
                .WithMessage("Image must not be empty");
        }
    }
}