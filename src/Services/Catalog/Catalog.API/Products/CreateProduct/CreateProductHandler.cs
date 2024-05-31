using MediatR;

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

    public record CreatedProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : IRequest<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler : IRequestHandler<CreatedProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreatedProductCommand request, CancellationToken cancellationToken)
        {
            /*
             * Perform business logic to create a product
             */
            throw new NotImplementedException();
        }
    }
}
