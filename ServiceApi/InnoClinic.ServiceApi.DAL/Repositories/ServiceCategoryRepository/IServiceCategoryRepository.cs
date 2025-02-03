using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Models;

namespace InnoClinic.ServiceApi.DataAccess.Repositories.ServiceCategoryRepository;

public interface IServiceCategoryRepository
{
    Task<ServiceCategory> Add(ServiceCategory service);
    Task<ServiceCategory> Update(ServiceCategory services);
    Task UpdateRange(List<ServiceCategory> services);
    Task<ServiceCategory?> Delete(string id);
    Task<ServiceCategory?> GetByIdAsync(string id);
    Task<List<ServiceCategory>> GetAllAsync(QueryObject query);
}