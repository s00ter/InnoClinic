using FluentValidation;
using InnoClinic.Prof.BusinessLogic.Dto.Specialization;

namespace InnoClinic.Prof.BusinessLogic.Validators.SpecializationValidators;

public class CreateSpecializationValidator : AbstractValidator<CreateSpecializationRequest>
{
    public CreateSpecializationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.IsActive)
            .NotEmpty().WithMessage("IsActive is required");
    }
}