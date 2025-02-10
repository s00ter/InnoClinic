using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Models;

namespace InnoClinic.ServiceApi.DataAccess.Repositories.ServiceRepository;

public interface IServiceRepository
{
    Task<Service> Add(Service service);
    Task<Service> Update(Service services);
    Task UpdateRange(List<Service> services);
    Task<Service?> Delete(string id);
    Task<Service?> GetByIdAsync(string id);
    Task<List<Service>> GetAllAsync(QueryObject query);
}