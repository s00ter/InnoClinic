namespace InnoClinic.Prof.DataAccess.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public bool IsLinkedToAccount { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
}