using Basket.API.Models;
using BuildingBlocks.CQRS;
using static BuildingBlocks.Behaviors.ValidationBehaviorConstants;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string userName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage(FIELD_NOT_NULL);
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage(FIELD_REQUIRED);
        }
    }

    internal class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            // TODO: Store basked in database
            // TODO: Update cache

            return new StoreBasketResult("carlos");
        }
    }
}
