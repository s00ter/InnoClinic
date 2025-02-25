namespace InnoClinic.AppointmentApi.BL.Exceptions;

public class AppointmentServiceException : Exception
{
    public required string Title { get; init; }
    public required string Details { get; init; }
    public required string Type { get; init; }
    public required string Instance { get; init; }
}