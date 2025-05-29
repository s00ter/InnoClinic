using System.Collections.Frozen;
using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;
using InnoClinic.ServiceApi.BusinessLogic.Mappers;
using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Repositories.ServiceCategoryRepository;
using InnoClinic.ServiceApi.DataAccess.Repositories.ServiceRepository;
using InnoClinic.Shared.Models;

namespace InnoClinic.ServiceApi.BusinessLogic.Services.ServiceService;

public class ServiceService(
    IServiceRepository serviceRepository,
    IServiceCategoryRepository serviceCategoryRepository
    ) : IServiceService
{
    public async Task<FrozenSet<ShowServiceResponse>> GetAllServicesAsync(QueryPaginationArguments query, 
        CancellationToken cancellationToken)
    {
        var services = await serviceRepository.GetAllAsync(query, cancellationToken);
        var res = services.Select(x => x.MapShowServiceDto()).ToFrozenSet();
        return res;
    }
    
    public async Task<ServiceInfoResponse> GetServiceInfoAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        var service = await serviceRepository.GetByIdAsync(id, cancellationToken);
        
        return service.MapServiceInfoDto();
    }
    
    public async Task<Service> CreateServiceAsync(CreateServiceRequest request, 
        CancellationToken cancellationToken)
    {
        var serCat = await serviceCategoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        
        var service = new Service
        {
            Id = Guid.NewGuid(),
            CategoryId = serCat.Id,
            Name = request.Name,
            Price = request.Price,
            SpecializationId = request.SpecializationId,
            IsActive = request.IsActive
        };
            
        return await serviceRepository.Add(service, cancellationToken);
    }
    
    public async Task<ShowServiceResponse> UpdateServiceAsync(Guid id, UpdateServiceRequest request, 
        CancellationToken cancellationToken)
    {
        var service = await serviceRepository.GetByIdAsync(id, cancellationToken);
        
        service.CategoryId = request.CategoryId;
        service.Name = request.Name;
        service.Price = request.Price;
        service.SpecializationId = request.SpecializationId;
        service.IsActive = request.IsActive;
        
        var res = await serviceRepository.Update(service, cancellationToken);

        return res.MapShowServiceDto();
    }
    
    public async Task<Service> DeleteServiceAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        var res = await serviceRepository.Delete(id, cancellationToken);
        
        return res;
    }
}