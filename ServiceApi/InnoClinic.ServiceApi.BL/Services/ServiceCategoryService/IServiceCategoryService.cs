using System.Collections.Frozen;
using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;
using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.Shared.Models;

namespace InnoClinic.ServiceApi.BusinessLogic.Services.ServiceCategoryService;

public interface IServiceCategoryService
{
    Task<FrozenSet<ShowServiceCategoryResponse>> GetAllServiceCategoriesAsync(QueryPaginationArguments query, 
        CancellationToken cancellationToken = default);
    Task<ServiceCategoryInfoResponse> GetServiceCategoryInfoAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task<ServiceCategory> CreateServiceCategoryAsync(CreateServiceCategoryRequest request, 
        CancellationToken cancellationToken = default);
    Task<ShowServiceCategoryResponse> UpdateServiceCategoryAsync(Guid id, UpdateServiceCategoryRequest request, 
        CancellationToken cancellationToken = default);
    Task<ServiceCategory?> DeleteServiceCategoryAsync(Guid id, 
        CancellationToken cancellationToken = default);
}