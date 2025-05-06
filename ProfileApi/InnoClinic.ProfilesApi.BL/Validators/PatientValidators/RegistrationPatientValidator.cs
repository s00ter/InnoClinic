using FluentValidation;
using InnoClinic.Prof.BusinessLogic.Dto.Patient;

namespace InnoClinic.Prof.BusinessLogic.Validators.PatientValidators;

public class RegistrationPatientValidator : AbstractValidator<RegistrationPatientRequest>
{
    public RegistrationPatientValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required");
        
        RuleFor(x => x.MiddleName)
            .NotEmpty().WithMessage("MiddleName is required");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth is required");
        
        RuleFor(x => x.isLinkedToAccount)
            .NotEmpty().WithMessage("isLinkedToAccount is required");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth is required");
    }
}