using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.DataAccess.Entities;

namespace InnoClinic.Prof.BusinessLogic.Mappers;

public static class SpecializationMapper
{
    public static ShowSpecializationDto MapShowSpecializationDto(this Specialization patient)
    {
        return new ShowSpecializationDto
        {
            Id = patient.Id,
            Name = patient.Name,
            IsActive = patient.IsActive
        };
    }
    
    public static SpecializationInfoDto MapSpecializationInfoDto(this Specialization patient)
    {
        return new SpecializationInfoDto
        {
            Id = patient.Id,
            Name = patient.Name,
            IsActive = patient.IsActive
        };
    }
}