using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;
using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Models;

namespace InnoClinic.ServiceApi.BusinessLogic.Services.ServiceCategoryService;

public interface IServiceCategoryService
{
    Task<List<ShowServiceCategoryDto>> GetAllServiceCategories(QueryObject query);
    Task<ServiceCategoryInfoDto> GetServiceCategoryInfo(string id);
    Task<ServiceCategory> CreateServiceCategory(CreateServiceCategoryDto dto);
    Task<ShowServiceCategoryDto> UpdateServiceCategory(string id, UpdateServiceCategoryDto dto);
    Task<ServiceCategory?> DeleteServiceCategory(string id);
}