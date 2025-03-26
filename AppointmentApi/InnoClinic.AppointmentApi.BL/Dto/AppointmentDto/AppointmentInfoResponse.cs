using InnoClinic.AppointmentApi.DataAccess.Entity;

namespace InnoClinic.AppointmentApi.BL.Dto.AppointmentDto;

public class AppointmentInfoResponse
{
    public required Guid Id { get; init; }
    public required Guid DoctorId { get; init; }
    public required Guid ServiceId { get; init; }
    public required DateTimeOffset? AppointmentDate { get; init; }
    public bool IsApproved { get; init; }
    
    public Result? Result { get; init; }
}