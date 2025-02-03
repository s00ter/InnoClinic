using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;

namespace InnoClinic.AppointmentApi.BL.Services.ResultService;

public interface IResultService
{
    Task<List<ShowResultDto>> GetAllResults(QueryObject query);
    Task<ResultInfoDto> GetResultInfo(string id);
    Task<Result> CreateResult(CreateResultDto dto);
    Task<ShowResultDto> UpdateResult(string id, UpdateResultDto dto);
    Task<Result?> DeleteResult(string id);
}