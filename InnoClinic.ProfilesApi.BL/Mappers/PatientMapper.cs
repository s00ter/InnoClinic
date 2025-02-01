using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.DataAccess.Entities;

namespace InnoClinic.Prof.BusinessLogic.Mappers;

public static class PatientMapper
{
    public static ShowPatientDto MapShowPatientDto(this Patient patient)
    {
        return new ShowPatientDto
        {
            Id = patient.Id,
            UserId = patient.UserId,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            MiddleName = patient.MiddleName,
            isLinkedToAccount = patient.IsLinkedToAccount
        };
    }
    
    public static PatientInfoDto MapPatientInfoDto(this Patient patient)
    {
        return new PatientInfoDto
        {
            Id = patient.Id,
            UserId = patient.UserId,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            MiddleName = patient.MiddleName,
            isLinkedToAccount = patient.IsLinkedToAccount,
            DateOfBirth = patient.DateOfBirth
        };
    }
}