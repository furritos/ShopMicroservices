using Basket.API.Models;
using BuildingBlocks.CQRS;
using static BuildingBlocks.Behaviors.ValidationBehaviorConstants;
using FluentValidation;
using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage(FIELD_NOT_NULL);
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage(FIELD_REQUIRED);
        }
    }

    internal class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart Cart = command.Cart;

            // TODO: Store basked in database
            // TODO: Update cache
            await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(command.Cart.UserName);
        }
    }
}
