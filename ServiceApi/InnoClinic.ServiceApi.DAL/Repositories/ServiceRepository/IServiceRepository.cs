using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.Shared.Models;

namespace InnoClinic.ServiceApi.DataAccess.Repositories.ServiceRepository;

public interface IServiceRepository
{
    Task<Service> Add(Service service, 
        CancellationToken cancellationToken = default);
    Task<Service> Update(Service services, 
        CancellationToken cancellationToken = default);
    Task<Service> Delete(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Service> GetByIdAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task<List<Service>> GetAllAsync(QueryPaginationArguments query, 
        CancellationToken cancellationToken = default);
}