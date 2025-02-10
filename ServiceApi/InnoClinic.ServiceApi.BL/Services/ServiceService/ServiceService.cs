using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;
using InnoClinic.ServiceApi.BusinessLogic.Mappers;
using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Models;
using InnoClinic.ServiceApi.DataAccess.Repositories.ServiceRepository;

namespace InnoClinic.ServiceApi.BusinessLogic.Services.ServiceService;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    public async Task<List<ShowServiceDto>> GetAllServices(QueryObject query)
    {
        var services = await serviceRepository.GetAllAsync(query);
        var res = services.Select(x => x.MapShowServiceDto()).ToList();
        return res;
    }
    
    public async Task<ServiceInfoDto> GetServiceInfo(string id)
    {
        var service = await serviceRepository.GetByIdAsync(id);
        if (service is null)
        {
            throw new NullReferenceException("Service not found");
        }
        return service.MapServiceInfoDto();
    }
    
    public async Task<Service> CreateService(CreateServiceDto dto)
    {
        var service = new Service
        {
            Id = Guid.NewGuid().ToString(),
            CategoryId = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Price = dto.Price,
            SpecializationId = dto.SpecializationId,
            IsActive = dto.IsActive
        };
            
        return await serviceRepository.Add(service);
    }
    
    public async Task<ShowServiceDto> UpdateService(string id, UpdateServiceDto dto)
    {
        var service = await serviceRepository.GetByIdAsync(id);
        if (service == null)
        { 
            throw new NullReferenceException("Service not found");
        }
        
        service.CategoryId = dto.CategoryId;
        service.Name = dto.Name;
        service.Price = dto.Price;
        service.SpecializationId = dto.SpecializationId;
        service.IsActive = dto.IsActive;
        
        var res = await serviceRepository.Update(service);

        return res.MapShowServiceDto();
    }
    
    public async Task<Service?> DeleteService(string id)
    {
        var res = await serviceRepository.Delete(id);
        if (res == null)
        {
            throw new NullReferenceException("Service not found");
        }
        return res;
    }
}