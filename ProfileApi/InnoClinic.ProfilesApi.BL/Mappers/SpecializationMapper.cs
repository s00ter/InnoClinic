using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.DataAccess.Entities;

namespace InnoClinic.Prof.BusinessLogic.Mappers;

public static class SpecializationMapper
{
    public static ShowSpecializationResponse MapShowSpecializationDto(this Specialization patient)
    {
        return new ShowSpecializationResponse
        {
            Id = patient.Id,
            Name = patient.Name,
            IsActive = patient.IsActive
        };
    }
    
    public static SpecializationInfoResponse MapSpecializationInfoDto(this Specialization patient)
    {
        return new SpecializationInfoResponse
        {
            Id = patient.Id,
            Name = patient.Name,
            IsActive = patient.IsActive
        };
    }
}