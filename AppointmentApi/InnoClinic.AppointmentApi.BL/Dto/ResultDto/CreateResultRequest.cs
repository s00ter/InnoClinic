namespace InnoClinic.AppointmentApi.BL.Dto.ResultDto;

public class CreateResultRequest
{
    public required string Complaints { get; init; }
    public required string Conclusion { get; init; }
    public required string Recomindations { get; init; }
    public required Guid AppointmentId { get; init; }
}