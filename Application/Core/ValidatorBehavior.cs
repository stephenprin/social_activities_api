using System.Reflection.Metadata.Ecma335;
using FluentValidation;
using MediatR;

namespace Application.Core;

public class ValidatorBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
 : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator == null) return await next();
        var validatorResults = await validator.ValidateAsync(request, cancellationToken);
        if(!validatorResults.IsValid)
        {
            throw new ValidationException(validatorResults.Errors);
        }

        return await next();

    }
}
