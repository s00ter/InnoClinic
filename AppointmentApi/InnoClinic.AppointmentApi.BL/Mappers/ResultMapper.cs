using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.DataAccess.Entity;

namespace InnoClinic.AppointmentApi.BL.Mappers;

public static class ResultMapper
{
    public static ShowResultResponse MapShowResultResponse(this Result doctor)
    {
        return new ShowResultResponse
        {
            Id = doctor.Id,
            Complaints = doctor.Complaints,
            Conclusion = doctor.Conclusion,
            Recomindations = doctor.Recomindations,
            AppointmentId = doctor.AppointmentId
        };
    }
    
    public static ResultInfoResponse MapResultInfoResponse(this Result doctor)
    {
        return new ResultInfoResponse
        {
            Id = doctor.Id,
            Complaints = doctor.Complaints,
            Conclusion = doctor.Conclusion,
            Recomindations = doctor.Recomindations,
            AppointmentId = doctor.AppointmentId
        };
    }
}