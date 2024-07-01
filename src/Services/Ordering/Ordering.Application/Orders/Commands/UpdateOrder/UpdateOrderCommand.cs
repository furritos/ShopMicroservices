using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.DTO;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDTO Order) : ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccess);
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("ID is Required");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer ID is Required");
        }
    }
}
