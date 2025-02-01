namespace InnoClinic.OfficesApi.BusinessLogic.Dto.Office;

public class UpdateOfficeDto
{
    public string? Address { get; set; }
    
    public string? PhotoId { get; set; }
    
    public string? RegistryPhoneNumber { get; set; }
    
    public bool IsActive { get; set; }
}