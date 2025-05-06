namespace InnoClinic.Shared.IventTypes;

public class DoctorCreated
{
    public Guid UserId { get; init; }
    public string Role { get; init; }
}