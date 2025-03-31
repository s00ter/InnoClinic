namespace InnoClinic.Prof.BusinessLogic.Dto.Patient;

public class ShowPatientResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public bool isLinkedToAccount { get; init; }
}