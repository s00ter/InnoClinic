namespace InnoClinic.Prof.BusinessLogic.Dto.Doctor;

public class UpdateDoctorRequest
{
    public string OfficeId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public Guid SpecializationId { get; init; }
}