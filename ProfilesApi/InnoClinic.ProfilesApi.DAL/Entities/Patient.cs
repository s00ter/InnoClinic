namespace InnoClinic.Prof.DataAccess.Entities;

public class Patient
{
    public string Id { get; set; }
    public string? UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public bool IsLinkedToAccount { get; set; }
    public DateTime DateOfBirth { get; set; }
}