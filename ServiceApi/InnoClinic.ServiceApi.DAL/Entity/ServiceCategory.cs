namespace InnoClinic.ServiceApi.DataAccess.Entity;

public class ServiceCategory
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int TimeSlotSize { get; set; } /*min*/
}