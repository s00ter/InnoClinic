using InnoClinic.Office.BusinessLogic.Dto.Office;
using InnoClinic.Office.BusinessLogic.Mappers;
using InnoClinic.Office.DataAccess.Repositories.OfficeRepository;
using InnoClinic.Shared.Models;

namespace InnoClinic.Office.BusinessLogic.Services.OfficeService;

public class OfficeService(
    IOfficeRepository officeRepository
    ) : IOfficeService
{
    public async Task<List<ShowOfficeResponse>> GetAllOffices(QueryPaginationArguments query, 
        CancellationToken cancellationToken)
    {
        var offices = await officeRepository.GetAllAsync(query, cancellationToken);
        var res = offices.Select(x => x.MapShowOfficeDto()).ToList();
        return res;
    }
    
    public async Task<OfficeInfoResponse> GetOfficeInfo(string id, 
        CancellationToken cancellationToken)
    {
        var patient = await officeRepository.GetByIdAsync(id, cancellationToken);
        
        return patient.MapOfficeInfoDto();
    }
    
    public async Task<OfficeResponse> CreateOffice(CreateOfficeRequest request, 
        CancellationToken cancellationToken)
    {
        var office = new DataAccess.Entities.Office
        {
            Address = request.Address,
            PhotoId = request.PhotoId,
            RegistryPhoneNumber = request.RegistryPhoneNumber,
            IsActive = request.IsActive,
        };
        
        var res = await officeRepository.Add(office, cancellationToken);

        return res.MapOfficeDto();
    }
    
    public async Task UpdateOffice(string id, UpdateOfficeRequest request, 
        CancellationToken cancellationToken)
    {
        var office = await officeRepository.GetByIdAsync(id, cancellationToken);
        
        office.Address = request.Address;
        office.PhotoId = request.PhotoId;
        office.RegistryPhoneNumber = request.RegistryPhoneNumber;
        office.IsActive = request.IsActive;
        
        await officeRepository.Update(office, cancellationToken);
    }
    
    public async Task DeleteOffice(string id, 
        CancellationToken cancellationToken)
    {
        await officeRepository.Delete(id, cancellationToken);
    }
}