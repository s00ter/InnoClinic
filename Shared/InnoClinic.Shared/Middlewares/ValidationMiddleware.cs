using System.Text;
using System.Text.Json;
using FluentValidation;
using InnoClinic.Shared.Exceptions;
using InnoClinic.Shared.Extensions;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Shared.Middlewares;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await next(context);
            return;
        }

        var requestType = endpoint.Metadata.OfType<TargetRequestTypeAttribute>().FirstOrDefault()?.RequestType;
        if (requestType == null)
        {
            await next(context);
            return;
        }

        var validatorType = typeof(IValidator<>).MakeGenericType(requestType);

        if (context.RequestServices.GetService(validatorType) is IValidator validator)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var bodyText = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (!string.IsNullOrWhiteSpace(bodyText))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new GuidConverter(),
                        new DateTimeOffsetConverter()
                    }
                };

                var body = JsonSerializer.Deserialize(bodyText, requestType, options);
                if (body != null)
                {
                    var validationContext = new ValidationContext<object>(body);
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
        }

        await next(context);
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class TargetRequestTypeAttribute(Type requestType) : Attribute
{
    public Type RequestType { get; } = requestType;
}