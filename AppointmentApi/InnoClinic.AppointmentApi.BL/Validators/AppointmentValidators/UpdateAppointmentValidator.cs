using FluentValidation;
using InnoClinic.AppointmentApi.BL.Dto.AppointmentDto;

namespace InnoClinic.AppointmentApi.BL.Validators.AppointmentValidators;

public class UpdateAppointmentValidator : AbstractValidator<UpdateAppointmentRequest>
{
    public UpdateAppointmentValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("DoctorId is required");
        
        RuleFor(x => x.ServiceId)
            .NotEmpty().WithMessage("ServiceId is required");
        
        RuleFor(x => x.DateTimeOffset)
            .NotEmpty().WithMessage("DateTimeOffset is required");
        
        RuleFor(x => x.IsApproved)
            .NotEmpty().WithMessage("IsApproved is required");
    }
}