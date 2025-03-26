namespace InnoClinic.AppointmentApi.BL.Dto.ResultDto;

public class UpdateResultRequest
{
    public string? Complaints { get; init; }
    public string? Conclusion { get; init; }
    public string? Recommendations { get; init; }
    public Guid AppointmentId { get; init; }
}