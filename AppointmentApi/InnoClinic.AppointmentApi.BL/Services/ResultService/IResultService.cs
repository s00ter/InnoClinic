using System.Collections.Frozen;
using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.BL.Services.ResultService;

public interface IResultService
{
    Task<FrozenSet<ShowResultResponse>> GetAllResults(QueryPaginationArguments queryPagination, CancellationToken cancellationToken = default);
    Task<ResultInfoResponse> GetResultInfo(Guid id, CancellationToken cancellationToken = default);
    Task CreateResult(CreateResultRequest request, CancellationToken cancellationToken = default);
    Task UpdateResult(Guid id, UpdateResultRequest request, CancellationToken cancellationToken = default);
    Task DeleteResult(Guid id, CancellationToken cancellationToken = default);
}