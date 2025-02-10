using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;
using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.ServiceApi.DataAccess.Models;

namespace InnoClinic.ServiceApi.BusinessLogic.Services.ServiceService;

public interface IServiceService
{
    Task<List<ShowServiceDto>> GetAllServices(QueryObject query);
    Task<ServiceInfoDto> GetServiceInfo(string id);
    Task<Service> CreateService(CreateServiceDto dto);
    Task<ShowServiceDto> UpdateService(string id, UpdateServiceDto dto);
    Task<Service?> DeleteService(string id);
}