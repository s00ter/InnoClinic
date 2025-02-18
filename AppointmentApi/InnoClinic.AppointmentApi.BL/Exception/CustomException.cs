namespace InnoClinic.AppointmentApi.BL.Exception;

public class CustomException : System.Exception
{
    public required string Title { get; init; }
    public required string Details { get; init; }
    public required string Type { get; init; }
    public required string Instance { get; init; }
}