using BuildingBlocks.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTO;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);

        var orderAsDTOs = ProjectToOrderDTO(orders);

        return new GetOrdersByNameResult(orderAsDTOs);
    }

    private List<OrderDTO> ProjectToOrderDTO(List<Order> orders)
    {
        List<OrderDTO> orderDTOs = new ();
        foreach (var order in orders)
        {
            var orderDTO = new OrderDTO(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDTO(
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.State,
                    order.ShippingAddress.ZipCode
                    ),
                BillingAddress: new AddressDTO(
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.State,
                    order.ShippingAddress.ZipCode
                    ),
                Payment: new PaymentDTO(
                    order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    order.Payment.PaymentMethod
                    ),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(oi => new OrderItemDTO(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
            orderDTOs.Add(orderDTO);
        }
        return orderDTOs;
    }
}