using FluentValidation;
using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;

namespace InnoClinic.ServiceApi.BusinessLogic.Validators.ServiceValidators;

public class CreateServiceValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        
        RuleFor(x => x.SpecializationId)
            .NotEmpty().WithMessage("SpecializationId is required");
        
        RuleFor(x => x.IsActive)
            .NotEmpty().WithMessage("IsActive is required");
    }
}