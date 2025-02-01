using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.DataAccess.Entities;

namespace InnoClinic.Prof.BusinessLogic.Mappers;

public static class DoctorMapper
{
    public static ShowDoctorDto MapShowDoctorDto(this Doctor doctor)
    {
        return new ShowDoctorDto
        {
            Id = doctor.Id,
            UserId = doctor.UserId,
            SpecializationId = doctor.SpecializationId,
            OfficeId = doctor.OfficeId,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            MiddleName = doctor.MiddleName,
        };
    }
    
    public static DoctorInfoDto MapDoctorInfoDto(this Doctor doctor)
    {
        return new DoctorInfoDto
        {
            Id = doctor.Id,
            UserId = doctor.UserId,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            MiddleName = doctor.MiddleName,
            DateOfBirth = doctor.DateOfBirth,
            SpecializationId = doctor.SpecializationId,
            OfficeId = doctor.OfficeId,
            CareerStartYear = doctor.CareerStartYear,
            Status = doctor.Status
        };
    }
}