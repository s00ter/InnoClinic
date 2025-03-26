using System.Collections.Frozen;
using InnoClinic.AppointmentApi.BL.Dto.ResultDto;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.Shared.Models;

namespace InnoClinic.AppointmentApi.BL.Services.ResultService;

public interface IResultService
{
    Task<FrozenSet<ShowResultResponse>> GetAllResults(QueryPaginationArguments queryPagination, CancellationToken cancellationToken = default);
    Task<ResultInfoResponse> GetResultInfo(Guid id, CancellationToken cancellationToken = default);
    Task<Result> CreateResult(CreateResultRequest request, CancellationToken cancellationToken = default);
    Task UpdateResult(Guid id, UpdateResultRequest request, CancellationToken cancellationToken = default);
    Task DeleteResult(Guid id, CancellationToken cancellationToken = default);
}