namespace InnoClinic.AppointmentApi.BL.Dto.AppointmentDto;

public class UpdateAppointmentRequest
{
    public Guid DoctorId { get; init; }
    public Guid ServiceId { get; init; }
    public DateTimeOffset DateTimeOffset { get; init; }
    public bool IsApproved { get; init; }
}