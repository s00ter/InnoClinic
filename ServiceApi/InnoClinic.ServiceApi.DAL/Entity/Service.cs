namespace InnoClinic.ServiceApi.DataAccess.Entity;

public class Service
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public string SpecializationId { get; set; }
    public bool IsActive { get; set; }
}