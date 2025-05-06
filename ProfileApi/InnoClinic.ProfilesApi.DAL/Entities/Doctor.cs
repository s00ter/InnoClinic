namespace InnoClinic.Prof.DataAccess.Entities;

public class Doctor
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public Guid SpecializationId { get; set; }
    public string OfficeId { get; set; }
    public DateTimeOffset CareerStartYear { get; set; }
    public string Status { get; set; }
}