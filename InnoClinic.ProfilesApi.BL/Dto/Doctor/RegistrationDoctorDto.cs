namespace InnoClinic.Prof.BusinessLogic.Dto.Doctor;

public class RegistrationDoctorDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string SpecializationId { get; set; }
    public string OfficeId { get; set; }
    public DateTime CareerStartYear { get; set; }
    public string Status { get; set; }
}