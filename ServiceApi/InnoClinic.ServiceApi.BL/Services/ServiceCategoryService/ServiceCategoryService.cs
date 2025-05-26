using System.Collections.Frozen;
using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;
using InnoClinic.ServiceApi.BusinessLogic.Mappers;
using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Repositories.ServiceCategoryRepository;
using InnoClinic.Shared.Models;

namespace InnoClinic.ServiceApi.BusinessLogic.Services.ServiceCategoryService;

public class ServiceCategoryService(
    IServiceCategoryRepository serviceCategoryRepository
    ) : IServiceCategoryService
{
    public async Task<FrozenSet<ShowServiceCategoryResponse>> GetAllServiceCategoriesAsync(QueryPaginationArguments query, 
        CancellationToken cancellationToken)
    {
        var services = await serviceCategoryRepository.GetAllAsync(query, cancellationToken);
        var res = services.Select(x => x.MapShowServiceCategoryDto()).ToFrozenSet();
        return res;
    }
    
    public async Task<ServiceCategoryInfoResponse> GetServiceCategoryInfoAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        var serviceCategory = await serviceCategoryRepository.GetByIdAsync(id, cancellationToken);
        
        return serviceCategory.MapServiceCategoryInfoDto();
    }
    
    public async Task<ServiceCategory> CreateServiceCategoryAsync(CreateServiceCategoryRequest request, 
        CancellationToken cancellationToken)
    {
        var service = new ServiceCategory
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            TimeSlotSize = request.TimeSlotSize,
        };
            
        return await serviceCategoryRepository.AddAsync(service, cancellationToken);
    }
    
    public async Task<ShowServiceCategoryResponse> UpdateServiceCategoryAsync(Guid id, UpdateServiceCategoryRequest request, 
        CancellationToken cancellationToken)
    {
        var serviceCategory = await serviceCategoryRepository.GetByIdAsync(id, cancellationToken);
        
        serviceCategory.Name = request.Name;
        serviceCategory.TimeSlotSize = request.TimeSlotSize;
        
        var res = await serviceCategoryRepository.Update(serviceCategory, cancellationToken);

        return res.MapShowServiceCategoryDto();
    }
    
    public async Task<ServiceCategory?> DeleteServiceCategoryAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        var res = await serviceCategoryRepository.Delete(id, cancellationToken);
        return res;
    }
}