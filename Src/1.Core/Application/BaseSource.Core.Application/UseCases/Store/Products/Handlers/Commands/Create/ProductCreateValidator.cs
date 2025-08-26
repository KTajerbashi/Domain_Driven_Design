namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create;

public class ProductCreateValidator : AbstractValidator<ProductCreateCommand>
{
    public ProductCreateValidator()
    {
        RuleFor(item => item.Title).MinimumLength(10).WithMessage("Title Is Small");
        RuleFor(item => item.Description).MinimumLength(10).WithMessage("Description Is Small");
    }
}
