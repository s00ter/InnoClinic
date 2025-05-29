using System.Collections.Frozen;
using InnoClinic.ServiceApi.BusinessLogic.Dto.Service;
using InnoClinic.ServiceApi.DataAccess.Entity;
using InnoClinic.Shared.Models;

namespace InnoClinic.ServiceApi.BusinessLogic.Services.ServiceService;

public interface IServiceService
{
    Task<FrozenSet<ShowServiceResponse>> GetAllServicesAsync(QueryPaginationArguments query, 
        CancellationToken cancellationToken = default);
    Task<ServiceInfoResponse> GetServiceInfoAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Service> CreateServiceAsync(CreateServiceRequest request, 
        CancellationToken cancellationToken = default);
    Task<ShowServiceResponse> UpdateServiceAsync(Guid id, UpdateServiceRequest request, 
        CancellationToken cancellationToken = default);
    Task<Service> DeleteServiceAsync(Guid id, 
        CancellationToken cancellationToken = default);
}