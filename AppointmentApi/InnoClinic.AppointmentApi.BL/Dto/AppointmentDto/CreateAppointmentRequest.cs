namespace InnoClinic.AppointmentApi.BL.Dto.AppointmentDto;

public class CreateAppointmentRequest
{
    public required Guid DoctorId { get; init; }
    public required Guid ServiceId { get; init; }
    public required DateTimeOffset DateTimeOffset { get; init; }
    public bool IsApproved { get; init; }
}