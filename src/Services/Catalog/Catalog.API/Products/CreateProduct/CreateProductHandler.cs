using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using Marten;
using static BuildingBlocks.Behaviors.ValidationBehaviorConstants;

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

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(FIELD_REQUIRED)
                .Length(2, 150).WithMessage(FieldLength(2,150));
            RuleFor(x => x.Category).NotEmpty().WithMessage(FIELD_REQUIRED);
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage(FIELD_REQUIRED);
            RuleFor(x => x.Price).GreaterThan(0).WithMessage(FIELD_GREATER_ZERO);
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            /*
             * Perform business logic to create a product by first creating Product entity from command object
             */
            logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);
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
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            /*
             * Return CreateProductResult result
             */
            return new CreateProductResult(product.Id);
        }
    }
}
