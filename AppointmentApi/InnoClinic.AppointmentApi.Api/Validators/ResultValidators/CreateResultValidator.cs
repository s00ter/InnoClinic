using FluentValidation;
using InnoClinic.AppointmentApi.BL.Dto.ResultDto;

namespace InnoClinic.AppointmentApi.Api.Validators.ResultValidators;

public class CreateResultValidator : AbstractValidator<CreateResultRequest>
{
    public CreateResultValidator()
    {
        RuleFor(x => x.Complaints)
            .NotEmpty().WithMessage("Complaints is required");
        
        RuleFor(x => x.Conclusion)
            .NotEmpty().WithMessage("Conclusion is required");
        
        RuleFor(x => x.Recommendations)
            .NotEmpty().WithMessage("Recommendations is required");
        
        RuleFor(x => x.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required");
    }
}