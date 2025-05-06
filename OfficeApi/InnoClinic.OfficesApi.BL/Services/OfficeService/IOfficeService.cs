using InnoClinic.Office.BusinessLogic.Dto.Office;
using InnoClinic.Shared.Models;

namespace InnoClinic.Office.BusinessLogic.Services.OfficeService;

public interface IOfficeService
{
    Task<List<ShowOfficeResponse>> GetAllOffices(QueryPaginationArguments query, 
        CancellationToken cancellationToken = default);
    Task<OfficeInfoResponse> GetOfficeInfo(string id, 
        CancellationToken cancellationToken = default);
    Task<OfficeResponse> CreateOffice(CreateOfficeRequest request, 
        CancellationToken cancellationToken = default);
    Task UpdateOffice(string id, UpdateOfficeRequest request, 
        CancellationToken cancellationToken = default);
    Task DeleteOffice(string id, 
        CancellationToken cancellationToken = default);
}