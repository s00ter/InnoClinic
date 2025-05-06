using System.Collections.Frozen;
using InnoClinic.Office.BusinessLogic.Dto.Office;
using InnoClinic.Shared.Models;

namespace InnoClinic.Office.BusinessLogic.Services.OfficeService;

public interface IOfficeService
{
    Task<FrozenSet<ShowOfficeResponse>> GetAllOfficesAsync(QueryPaginationArguments query, 
        CancellationToken cancellationToken = default);
    Task<OfficeInfoResponse> GetOfficeInfoAsync(string id, 
        CancellationToken cancellationToken = default);
    Task<OfficeResponse> CreateOfficeAsync(CreateOfficeRequest request, 
        CancellationToken cancellationToken = default);
    Task UpdateOfficeAsync(string id, UpdateOfficeRequest request, 
        CancellationToken cancellationToken = default);
    Task DeleteOfficeAsync(string id, 
        CancellationToken cancellationToken = default);
}