namespace InnoClinic.AppointmentApi.BL.Dto.Appointment;

public class AppointmentInfoResponse
{
    public string Id { get; set; }
    public string DoctorId { get; set; }
    public string ServiceId { get; set; }
    public DateTime Date { get; set; }
    public DateTime Time { get; set; }
    public bool IsApproved { get; set; }
}