using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.BL.Mappers;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;
using InnoClinic.AppointmentApi.DataAccess.Repositories.ResultRepository;

namespace InnoClinic.AppointmentApi.BL.Services.ResultService;

public class ResultService(IResultRepository resultRepository) : IResultService
{
    public async Task<List<ShowResultDto>> GetAllResults(QueryObject query)
    {
        var doctors = await resultRepository.GetAllAsync(query);
        var res = doctors.Select(x => x.MapShowResultDto()).ToList();
        return res;
    }
    
    public async Task<ResultInfoDto> GetResultInfo(string id)
    {
        var doctor = await resultRepository.GetByIdAsync(id);
        if (doctor is null)
        {
            throw new NullReferenceException("Result not found");
        }
        return doctor.MapResultInfoDto();
    }
    
    public async Task<Result> CreateResult(CreateResultDto dto)
    {
        var doctor = new Result
        {
            Id = Guid.NewGuid().ToString(),
            Complaints = dto.Complaints,
            Conclusion = dto.Conclusion,
            Recomindations = dto.Recomindations,
            AppointmentId = dto.AppointmentId
        };
            
        return await resultRepository.Add(doctor);
    }
    
    public async Task<ShowResultDto> UpdateResult(string id, UpdateResultDto dto)
    {
        var dbDoctor = await resultRepository.GetByIdAsync(id);
        if (dbDoctor == null)
        { 
            throw new NullReferenceException("Result not found");
        }
        
        dbDoctor.Complaints = dto.Complaints;
        dbDoctor.Conclusion = dto.Conclusion;
        dbDoctor.Recomindations = dto.Recomindations;
        dbDoctor.AppointmentId = dto.AppointmentId;
        
        var res = await resultRepository.Update(dbDoctor);

        return res.MapShowResultDto();
    }
    
    public async Task<Result?> DeleteResult(string id)
    {
        var dbDoctor = await resultRepository.Delete(id);
        if (dbDoctor == null)
        {
            throw new NullReferenceException("Result not found");
        }
        return dbDoctor;
    }
}