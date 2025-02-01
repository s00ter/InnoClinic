using InnoClinic.OfficesApi.BusinessLogic.Dto.Office;

namespace InnoClinic.OfficesApi.BusinessLogic.Mappers;

public static class OfficeMapper
{
    public static ShowOfficeDto MapShowOfficeDto(this DataAccess.Entities.Office office)
    {
        return new ShowOfficeDto
        {
            Id = office.Id.ToString(),
            Address = office.Address,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }
    
    public static OfficeInfoDto MapOfficeInfoDto(this DataAccess.Entities.Office office)
    {
        return new OfficeInfoDto
        {
            Id = office.Id.ToString(),
            Address = office.Address,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }
    
    public static OfficeDto MapOfficeDto(this DataAccess.Entities.Office office)
    {
        return new OfficeDto
        {
            Id = office.Id.ToString(),
            Address = office.Address,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }
}