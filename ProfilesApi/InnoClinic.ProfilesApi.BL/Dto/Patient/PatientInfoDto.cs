namespace InnoClinic.Prof.BusinessLogic.Dto.Patient;

public class PatientInfoDto
{
    public string Id { get; set; }
    public string? UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public bool isLinkedToAccount { get; set; }
    public DateTime DateOfBirth { get; set; }
}