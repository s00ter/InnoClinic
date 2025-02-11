using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.BL.Services.ResultService;

public interface IResultService
{
    Task<List<ShowResultResponse>> GetAllResults(QueryObject query);
    Task<ResultInfoResponse> GetResultInfo(string id);
    Task<Result> CreateResult(CreateResultRequest request);
    Task<ShowResultResponse> UpdateResult(string id, UpdateResultRequest request);
    Task<Result?> DeleteResult(string id);
}