using FluentValidation;
using InnoClinic.Prof.BusinessLogic.Dto.Doctor;

namespace InnoClinic.Prof.BusinessLogic.Validators.DoctorValidators;

public class RegistrationDoctorValidator : AbstractValidator<RegistrationDoctorRequest>
{
    public RegistrationDoctorValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required");
        
        RuleFor(x => x.MiddleName)
            .NotEmpty().WithMessage("MiddleName is required");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth is required");
        
        RuleFor(x => x.SpecializationId)
            .NotEmpty().WithMessage("SpecializationId is required");
        
        RuleFor(x => x.OfficeId)
            .NotEmpty().WithMessage("OfficeId is required");
        
        RuleFor(x => x.CareerStartYear)
            .NotEmpty().WithMessage("CareerStartYear is required");
        
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required");
    }
}