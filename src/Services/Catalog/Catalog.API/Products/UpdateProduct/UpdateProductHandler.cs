namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.")
            .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Product category is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Product image file is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
    }
}

public class UpdateProductHandler(IDocumentSession session) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        Product? existingProduct = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (existingProduct == null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        else
        {
            existingProduct.Name = command.Name;
            existingProduct.Category = command.Category;
            existingProduct.Description = command.Description;
            existingProduct.ImageFile = command.ImageFile;
            existingProduct.Price = command.Price;

            session.Update(existingProduct);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}

