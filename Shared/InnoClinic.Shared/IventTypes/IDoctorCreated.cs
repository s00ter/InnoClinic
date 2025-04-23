namespace InnoClinic.Shared.IventTypes;

public interface IDoctorCreated
{
    public Guid UserId { get; set; }
    public string Role { get; set; }
}