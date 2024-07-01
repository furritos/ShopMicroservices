using BuildingBlocks.CQRS;
using Ordering.Application.DTO;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;
    public record GetOrdersByCustomerResult(IEnumerable<OrderDTO> orders);
}
