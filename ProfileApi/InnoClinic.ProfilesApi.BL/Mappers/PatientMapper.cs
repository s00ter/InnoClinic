using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.DataAccess.Entities;

namespace InnoClinic.Prof.BusinessLogic.Mappers;

public static class PatientMapper
{
    public static ShowPatientResponse MapShowPatientDto(this Patient patient)
    {
        return new ShowPatientResponse
        {
            Id = patient.Id,
            UserId = patient.UserId,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            MiddleName = patient.MiddleName,
            isLinkedToAccount = patient.IsLinkedToAccount
        };
    }
    
    public static PatientInfoResponse MapPatientInfoDto(this Patient patient)
    {
        return new PatientInfoResponse
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