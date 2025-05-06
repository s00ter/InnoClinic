using FluentValidation;
using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;

namespace InnoClinic.Prof.BusinessLogic.Validators.ReceptionistValidators;

public class RegistrationReceptionistValidator : AbstractValidator<RegistrationReceptionistRequest>
{
    public RegistrationReceptionistValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required");
        
        RuleFor(x => x.MiddleName)
            .NotEmpty().WithMessage("MiddleName is required");
        
        RuleFor(x => x.OfficeId)
            .NotEmpty().WithMessage("OfficeId is required");
    }
}