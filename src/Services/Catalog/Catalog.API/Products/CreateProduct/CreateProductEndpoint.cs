using Carter;

namespace Catalog.API.Products.CreateProduct
{
    /*
     * VERTICAL SLICE: This class represents our presentation layer
     * 
     * The folder, CreateProduct is our Vertical Slice feature
     * 
    */

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price) { }

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}
