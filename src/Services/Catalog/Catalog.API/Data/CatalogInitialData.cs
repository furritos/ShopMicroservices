using Catalog.API.Models;
using Marten;
using Marten.Schema;

namespace Catalog.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        // If there's any data, just return
        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private IEnumerable<Product> GetPreconfiguredProducts()
    {
        Random rnd = new Random();
        var categories = new List<string> { "electronics", "household", "clothes", "garage", "automobile", "computers" };
        var products = new List<Product>();
        for (int i = 0; i < 10; i++)
        {
            products.Add(new Product
            {
                Id = new Guid(string.Format("5ef237cb-1a59-4f75-aa1b-c99f3784f79{0}", i)),
                Name = string.Format("Sample Product {0}", i),
                Description = string.Format("Sample description text of Product {0}", i),
                ImageFile = string.Format("product-{0}.jpg", i),
                Price = (decimal)(1.99 + i),
                Category = categories.GetRange(0, rnd.Next(categories.Count) + 1)
            });
        }
        return products;
    }
}