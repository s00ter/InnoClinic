namespace InnoClinic.OfficesApi.BusinessLogic.Dto.Office;

public class OfficeDto
{
    public string Id { get; set; }
    
    public string? Address { get; set; }
    
    public string? PhotoId { get; set; }
    
    public string? RegistryPhoneNumber { get; set; }
    
    public bool IsActive { get; set; }
}