using FluentValidation;
using InnoClinic.AppointmentApi.BL.Dto.ResultDto;

namespace InnoClinic.AppointmentApi.BL.Validators.ResultValidators;

public class CreateResultValidator : AbstractValidator<CreateResultRequest>
{
    public CreateResultValidator()
    {
        RuleFor(x => x.Complaints)
            .NotEmpty().WithMessage("Complaints is required")
            .MaximumLength(2048).WithMessage("No more than 2048 characters");

        RuleFor(x => x.Conclusion)
            .NotEmpty().WithMessage("Conclusion is required")
            .MaximumLength(2048).WithMessage("No more than 2048 characters");

        RuleFor(x => x.Recommendations)
            .NotEmpty().WithMessage("Recommendations is required")
            .MaximumLength(2048).WithMessage("No more than 2048 characters");

        RuleFor(x => x.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required");
    }
}