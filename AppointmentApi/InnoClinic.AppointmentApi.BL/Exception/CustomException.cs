namespace InnoClinic.AppointmentApi.BL.Exception;

public class CustomException : System.Exception
{
    public string Title { get; set; }
    public string Details { get; set; }
    public string Type { get; set; }
    public string Instance { get; set; }
    public string AdditionalInfo { get; set; }
}