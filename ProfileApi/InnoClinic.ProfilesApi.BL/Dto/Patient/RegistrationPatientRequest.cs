namespace InnoClinic.Prof.BusinessLogic.Dto.Patient;

public class RegistrationPatientRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public bool isLinkedToAccount { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
}