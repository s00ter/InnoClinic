using InnoClinic.AppointmentApi.BL.Dto.ResultDto;
using InnoClinic.AppointmentApi.DataAccess.Entity;

namespace InnoClinic.AppointmentApi.BL.Mappers;

public static class ResultMapper
{
    public static ShowResultResponse MapShowResultResponse(this Result result)
    {
        return new ShowResultResponse
        {
            Id = result.Id,
            Complaints = result.Complaints,
            Conclusion = result.Conclusion,
            Recomindations = result.Recomindations,
            AppointmentId = result.AppointmentId
        };
    }
    
    public static ResultInfoResponse MapResultInfoResponse(this Result result)
    {
        return new ResultInfoResponse
        {
            Id = result.Id,
            Complaints = result.Complaints,
            Conclusion = result.Conclusion,
            Recomindations = result.Recomindations,
            AppointmentId = result.AppointmentId
        };
    }
}