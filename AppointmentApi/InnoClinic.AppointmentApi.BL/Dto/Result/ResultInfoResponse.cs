namespace InnoClinic.AppointmentApi.BL.Dto.Result;

public class ResultInfoResponse
{
    public required Guid Id { get; init; }
    public required string Complaints { get; init; }
    public required string Conclusion { get; init; }
    public required string Recomindations { get; init; }
    public required Guid AppointmentId { get; init; }
}