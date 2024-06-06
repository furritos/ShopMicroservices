using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;

namespace BuildingBlocks.Behaviors

{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TwoFactorResponse> where TRequest : ICommand<TResponse>
    {
        public async Task<TwoFactorResponse> Handle(TRequest request, RequestHandlerDelegate<TwoFactorResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}