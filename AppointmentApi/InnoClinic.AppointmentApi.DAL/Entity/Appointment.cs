namespace InnoClinic.AppointmentApi.DataAccess.Entity;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public bool IsApproved { get; set; }
    
    public Result? Result { get; set; }
}