namespace InnoClinic.Prof.BusinessLogic.Dto.Doctor;

public class DoctorInfoResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
    public Guid SpecializationId { get; init; }
    public string OfficeId { get; init; }
    public DateTimeOffset CareerStartYear { get; init; }
    public string Status { get; init; }
}