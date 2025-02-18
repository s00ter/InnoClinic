namespace InnoClinic.AppointmentApi.DataAccess.Entity;

public class Result
{
    public Guid Id { get; set; }
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recomindations { get; set; }
    public Guid AppointmentId { get; set; }
    
    public Appointment Appointment { get; set; }
}