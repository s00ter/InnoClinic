namespace InnoClinic.AppointmentApi.DataAccess.Models;

public class QueryObject
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}