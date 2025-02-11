using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.BL.Mappers;
using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;
using InnoClinic.AppointmentApi.DataAccess.Repositories.ResultRepository;

namespace InnoClinic.AppointmentApi.BL.Services.ResultService;

public class ResultService(IResultRepository resultRepository) : IResultService
{
    public async Task<List<ShowResultResponse>> GetAllResults(QueryObject query)
    {
        var doctors = await resultRepository.GetAllAsync(query);
        var res = doctors.Select(x => x.MapShowResultResponse()).ToList();
        return res;
    }
    
    public async Task<ResultInfoResponse> GetResultInfo(string id)
    {
        var doctor = await resultRepository.GetByIdAsync(id);
        if (doctor is null)
        {
            throw new NullReferenceException("Result not found");
        }
        return doctor.MapResultInfoResponse();
    }
    
    public async Task<Result> CreateResult(CreateResultRequest request)
    {
        var doctor = new Result
        {
            Id = Guid.NewGuid().ToString(),
            Complaints = request.Complaints,
            Conclusion = request.Conclusion,
            Recomindations = request.Recomindations,
            AppointmentId = request.AppointmentId
        };
            
        return await resultRepository.Add(doctor);
    }
    
    public async Task<ShowResultResponse> UpdateResult(string id, UpdateResultRequest request)
    {
        var dbDoctor = await resultRepository.GetByIdAsync(id);
        if (dbDoctor == null)
        { 
            throw new NullReferenceException("Result not found");
        }
        
        dbDoctor.Complaints = request.Complaints;
        dbDoctor.Conclusion = request.Conclusion;
        dbDoctor.Recomindations = request.Recomindations;
        dbDoctor.AppointmentId = request.AppointmentId;
        
        var res = await resultRepository.Update(dbDoctor);

        return res.MapShowResultResponse();
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