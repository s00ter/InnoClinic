namespace InnoClinic.AppointmentApi.BL.Dto.Result;

public class UpdateResultRequest
{
    public required string Complaints { get; init; }
    public required string Conclusion { get; init; }
    public required string Recomindations { get; init; }
    public required Guid AppointmentId { get; init; }
}