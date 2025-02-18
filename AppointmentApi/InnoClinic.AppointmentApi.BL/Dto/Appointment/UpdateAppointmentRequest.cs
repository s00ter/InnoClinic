namespace InnoClinic.AppointmentApi.BL.Dto.Appointment;

public class UpdateAppointmentRequest
{
    public required Guid DoctorId { get; init; }
    public required Guid ServiceId { get; init; }
    public required DateOnly Date { get; init; }
    public required TimeOnly Time { get; init; }
    public bool IsApproved { get; init; }
}