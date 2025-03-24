using FluentValidation;
using InnoClinic.Shared.Exceptions;
using MediatR;

namespace InnoClinic.Application.Behaviors;

public class ValidationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
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
            .Select(v => v.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .GroupBy(
                x => x.PropertyName.Substring(x.PropertyName.IndexOf('.') + 1),
                x => x.ErrorMessage, (propertyName, errorMessages) => new
                {
                    Key = propertyName, 
                    Values = errorMessages.Distinct().ToArray()
                }).ToDictionary(x => x.Key, x => x.Values);
        
        if (errorDictionary.Any())
            throw new ValidationAppException(errorDictionary);
        
        return await next();
    }
}