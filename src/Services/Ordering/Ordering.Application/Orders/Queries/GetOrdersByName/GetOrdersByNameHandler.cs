﻿using BuildingBlocks.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTO;
using Ordering.Application.Extensions;
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

        return new GetOrdersByNameResult(orders.ToOrderDTOList());
    }
}