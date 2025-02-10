using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;
using InnoClinic.ServiceApi.BusinessLogic.Mappers;
using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Models;
using InnoClinic.ServiceApi.DataAccess.Repositories.ServiceCategoryRepository;

namespace InnoClinic.ServiceApi.BusinessLogic.Services.ServiceCategoryService;

public class ServiceCategoryService(
    IServiceCategoryRepository serviceCategoryRepository
    ) : IServiceCategoryService
{
    public async Task<List<ShowServiceCategoryDto>> GetAllServiceCategories(QueryObject query)
    {
        var services = await serviceCategoryRepository.GetAllAsync(query);
        var res = services.Select(x => x.MapShowServiceCategoryDto()).ToList();
        return res;
    }
    
    public async Task<ServiceCategoryInfoDto> GetServiceCategoryInfo(string id)
    {
        var serviceCategory = await serviceCategoryRepository.GetByIdAsync(id);
        if (serviceCategory is null)
        {
            throw new NullReferenceException("ServiceCategory not found");
        }
        return serviceCategory.MapServiceCategoryInfoDto();
    }
    
    public async Task<ServiceCategory> CreateServiceCategory(CreateServiceCategoryDto dto)
    {
        var service = new ServiceCategory
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            TimeSlotSize = dto.TimeSlotSize,
        };
            
        return await serviceCategoryRepository.Add(service);
    }
    
    public async Task<ShowServiceCategoryDto> UpdateServiceCategory(string id, UpdateServiceCategoryDto dto)
    {
        var serviceCategory = await serviceCategoryRepository.GetByIdAsync(id);
        if (serviceCategory == null)
        { 
            throw new NullReferenceException("ServiceCategory not found");
        }
        
        serviceCategory.Name = dto.Name;
        serviceCategory.TimeSlotSize = dto.TimeSlotSize;
        
        var res = await serviceCategoryRepository.Update(serviceCategory);

        return res.MapShowServiceCategoryDto();
    }
    
    public async Task<ServiceCategory?> DeleteServiceCategory(string id)
    {
        var res = await serviceCategoryRepository.Delete(id);
        if (res == null)
        {
            throw new NullReferenceException("ServiceCategory not found");
        }
        return res;
    }
}