using BuildingBlocks.CQRS;
using Ordering.Application.DTO;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public record GetOrdersByNameQuery(string Name)
: IQuery<GetOrdersByNameResult>;

public record GetOrdersByNameResult(IEnumerable<OrderDTO> Orders);