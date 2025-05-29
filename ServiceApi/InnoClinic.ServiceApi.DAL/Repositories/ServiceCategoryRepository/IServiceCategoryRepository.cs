using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.Shared.Models;

namespace InnoClinic.ServiceApi.DataAccess.Repositories.ServiceCategoryRepository;

public interface IServiceCategoryRepository
{
    Task<ServiceCategory> AddAsync(ServiceCategory service, 
        CancellationToken cancellationToken = default);
    Task<ServiceCategory> Update(ServiceCategory services, 
        CancellationToken cancellationToken = default);
    Task<ServiceCategory> Delete(Guid id, 
        CancellationToken cancellationToken = default);
    Task<ServiceCategory> GetByIdAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task<List<ServiceCategory>> GetAllAsync(QueryPaginationArguments query, 
        CancellationToken cancellationToken = default);
}