using FluentValidation;
using InnoClinic.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InnoClinic.Shared.Filters;

public class GlobalValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments)
        {
            var model = argument.Value;

            if (model == null)
            {
                context.Result = new BadRequestObjectResult(new { Errors = new[] { $"Argument '{argument.Key}' is null." } });
                return;
            }

            var validatorType = typeof(IValidator<>).MakeGenericType(model.GetType());
            var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

            if (validator != null)
            {
                var validationContext = new ValidationContext<object>(model);
                var validationResult = await validator.ValidateAsync(validationContext);

                if (!validationResult.IsValid)
                {
                    var errorDictionary = validationResult.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).Distinct().ToArray());

                    throw new ValidationAppException(errorDictionary);
                }
            }
        }

        await next();
    }
}