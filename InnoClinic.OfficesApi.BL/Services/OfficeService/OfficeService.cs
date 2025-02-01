using InnoClinic.OfficesApi.BusinessLogic.Mappers;
using InnoClinic.OfficesApi.BusinessLogic.Dto.Office;
using InnoClinic.OfficesApi.DataAccess.Models;
using InnoClinic.OfficesApi.DataAccess.Repositories.OfficeRepository;
using MongoDB.Bson;

namespace InnoClinic.OfficesApi.BusinessLogic.Services.OfficeService;

public class OfficeService(
    IOfficeRepository officeRepository
    ) : IOfficeService
{
    public async Task<List<ShowOfficeDto>> GetAllOffices(QueryObject query)
    {
        var offices = await officeRepository.GetAllAsync(query);
        var res = offices.Select(x => x.MapShowOfficeDto()).ToList();
        return res;
    }
    
    public async Task<OfficeInfoDto> GetOfficeInfo(ObjectId id)
    {
        var patient = await officeRepository.GetByIdAsync(id);
        if (patient is null)
        {
            throw new NullReferenceException("Office not found");
        }
        return patient.MapOfficeInfoDto();
    }
    
    public async Task<OfficeDto> CreateOffice(CreateOfficeDto dto)
    {
        var office = new DataAccess.Entities.Office
        {
            Address = dto.Address,
            PhotoId = dto.PhotoId,
            RegistryPhoneNumber = dto.RegistryPhoneNumber,
            IsActive = dto.IsActive,
        };
        
        var res = await officeRepository.Add(office);

        return res.MapOfficeDto();
    }
    
    public async Task<ShowOfficeDto> UpdateOffice(ObjectId id, UpdateOfficeDto dto)
    {
        var office = await officeRepository.GetByIdAsync(id);
        if (office == null)
        { 
            throw new NullReferenceException("Office not found");
        }
        
        office.Address = dto.Address;
        office.PhotoId = dto.PhotoId;
        office.RegistryPhoneNumber = dto.RegistryPhoneNumber;
        office.IsActive = dto.IsActive;
        
        var res = await officeRepository.Update(office);

        return res.MapShowOfficeDto();
    }
    
    public async Task<OfficeDto?> DeleteOffice(ObjectId id)
    {
        var res = await officeRepository.Delete(id);
        if (res == null)
        {
            throw new NullReferenceException("Office not found");
        }
        return res.MapOfficeDto();
    }
}