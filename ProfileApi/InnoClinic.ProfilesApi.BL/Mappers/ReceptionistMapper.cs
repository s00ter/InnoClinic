using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.DataAccess.Entities;

namespace InnoClinic.Prof.BusinessLogic.Mappers;

public static class ReceptionistMapper
{
    public static ShowReceptionistResponse MapShowReceptionistDto(this Receptionist patient)
    {
        return new ShowReceptionistResponse
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            MiddleName = patient.MiddleName,
            AccountId = patient.AccountId,
            OfficeId = patient.OfficeId
        };
    }
    
    public static ReceptionistInfoResponse MapReceptionistInfoDto(this Receptionist patient)
    {
        return new ReceptionistInfoResponse
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