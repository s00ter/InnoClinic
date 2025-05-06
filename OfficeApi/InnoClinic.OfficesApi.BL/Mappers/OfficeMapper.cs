using InnoClinic.Office.BusinessLogic.Dto.Office;

namespace InnoClinic.Office.BusinessLogic.Mappers;

public static class OfficeMapper
{
    public static ShowOfficeResponse MapShowOfficeDto(this DataAccess.Entities.Office office)
    {
        return new ShowOfficeResponse
        {
            Id = office.Id.ToString(),
            Address = office.Address,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }
    
    public static OfficeInfoResponse MapOfficeInfoDto(this DataAccess.Entities.Office office)
    {
        return new OfficeInfoResponse
        {
            Id = office.Id.ToString(),
            Address = office.Address,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }
    
    public static OfficeResponse MapOfficeDto(this DataAccess.Entities.Office office)
    {
        return new OfficeResponse
        {
            Id = office.Id.ToString(),
            Address = office.Address,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }
}