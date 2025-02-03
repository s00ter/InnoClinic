namespace InnoClinic.Prof.BusinessLogic.Dto.Patient;

public class RegistrationPatientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public bool isLinkedToAccount { get; set; }
    public DateTime DateOfBirth { get; set; }
}