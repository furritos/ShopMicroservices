using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Ordering.Application.DTO;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginationResult<OrderDTO> Orders);
}
