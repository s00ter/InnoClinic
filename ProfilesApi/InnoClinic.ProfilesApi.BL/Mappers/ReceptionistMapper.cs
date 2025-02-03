using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.DataAccess.Entities;

namespace InnoClinic.Prof.BusinessLogic.Mappers;

public static class ReceptionistMapper
{
    public static ShowReceptionistDto MapShowReceptionistDto(this Receptionist patient)
    {
        return new ShowReceptionistDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            MiddleName = patient.MiddleName,
            AccountId = patient.AccountId,
            OfficeId = patient.OfficeId
        };
    }
    
    public static ReceptionistInfoDto MapReceptionistInfoDto(this Receptionist patient)
    {
        return new ReceptionistInfoDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            MiddleName = patient.MiddleName,
            AccountId = patient.AccountId,
            OfficeId = patient.OfficeId
        };
    }
}