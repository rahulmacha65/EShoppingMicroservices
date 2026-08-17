namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling UpdateProductCommand for product: {Name}", command.Name);

        Product? existingProduct = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (existingProduct == null)
        {
            logger.LogWarning("Product with name {Name} not found.", command.Name);
            throw new ProductNotFoundException();
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

            logger.LogInformation("Product with name {Name} updated successfully.", command.Name);
            return new UpdateProductResult(true);
        }
    }
}

