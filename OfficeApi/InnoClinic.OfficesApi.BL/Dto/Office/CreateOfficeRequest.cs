namespace InnoClinic.Office.BusinessLogic.Dto.Office;

public class CreateOfficeRequest
{
    public string Address { get; init; }
    
    public string PhotoId { get; init; }
    
    public string RegistryPhoneNumber { get; init; }
    
    public bool IsActive { get; init; }
}