using FluentValidation;
using InnoClinic.Shared.Exceptions;
using MediatR;

namespace InnoClinic.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();
        
        var context = new ValidationContext<TRequest>(request);
        
        var errorDictionary = _validators
            .SelectMany(v => v.Validate(context).Errors)
            .Where(x => x is not null)
            .GroupBy(x => x.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).Distinct().ToArray());
        
        return errorDictionary.Any() 
            ? throw new ValidationAppException(errorDictionary) 
            : await next();
    }
}
