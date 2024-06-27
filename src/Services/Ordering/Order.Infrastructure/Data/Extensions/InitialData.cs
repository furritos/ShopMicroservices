using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("3722996d-7c39-4bad-9c60-683f312298c9")), "john", "john@protomail.com"),
            Customer.Create(CustomerId.Of(new Guid("33964a9a-3d0d-451f-83f6-a0ae549369df")), "kevin", "kevin@protomail.com")
        };
    }
}
