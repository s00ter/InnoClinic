namespace InnoClinic.Prof.BusinessLogic.Dto.Patient;

public class PatientInfoResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public bool isLinkedToAccount { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
}