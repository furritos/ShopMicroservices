using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    /*
     * VERTICAL SLICE: This class represents our application logic layer
     * 
     * The folder, CreateProduct is our Vertical Slice feature
     * 
     * CQRS:  This handler class will be responsible for executing command logic 
     * using MediatR library.  It will receive the "command" and process it, encapsulate
     * business logic for specific operations.
     * 
    */

    public record CreatedProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler 
        : ICommandHandler<CreatedProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreatedProductCommand command, CancellationToken cancellationToken)
        {
            /*
             * Perform business logic to create a product by first creating Product entity from command object
             */
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            /*
             * Save to database
             */


            /*
             * Return CreateProductResult result
             */
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
