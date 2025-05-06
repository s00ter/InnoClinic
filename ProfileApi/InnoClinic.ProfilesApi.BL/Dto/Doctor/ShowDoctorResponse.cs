namespace InnoClinic.Prof.BusinessLogic.Dto.Doctor;

public class ShowDoctorResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string OfficeId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public Guid SpecializationId { get; init; }
}