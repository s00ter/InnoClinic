using FluentValidation;
using InnoClinic.Office.BusinessLogic.Dto.Office;

namespace InnoClinic.Office.BusinessLogic.Validators;

public class UpdateOfficeValidator : AbstractValidator<UpdateOfficeRequest>
{
    public UpdateOfficeValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required");
        
        RuleFor(x => x.PhotoId)
            .NotEmpty().WithMessage("PhotoId is required");
        
        RuleFor(x => x.RegistryPhoneNumber)
            .NotEmpty().WithMessage("RegistryPhoneNumber is required");
        
        RuleFor(x => x.IsActive)
            .NotEmpty().WithMessage("IsActive is required");
    }
}