using InnoClinic.AppointmentApi.BL.Dto.Result;
using InnoClinic.AppointmentApi.DataAccess.Entity;

namespace InnoClinic.AppointmentApi.BL.Mappers;

public static class ResultMapper
{
    public static ShowResultDto MapShowResultDto(this Result doctor)
    {
        return new ShowResultDto
        {
            Id = doctor.Id,
            Complaints = doctor.Complaints,
            Conclusion = doctor.Conclusion,
            Recomindations = doctor.Recomindations,
            AppointmentId = doctor.AppointmentId
        };
    }
    
    public static ResultInfoDto MapResultInfoDto(this Result doctor)
    {
        return new ResultInfoDto
        {
            Id = doctor.Id,
            Complaints = doctor.Complaints,
            Conclusion = doctor.Conclusion,
            Recomindations = doctor.Recomindations,
            AppointmentId = doctor.AppointmentId
        };
    }
}