﻿using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.DTO;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDTO Order) : ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid OId);
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
}
