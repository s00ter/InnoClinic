using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using InnoClinic.Prof.DataAccess.Repositories.DoctorRepository;

namespace InnoClinic.Prof.BusinessLogic.Services.DoctorService;

public class DoctorService(
    IDoctorRepository doctorRepository
    ) : IDoctorService
{
    public async Task<List<ShowDoctorDto>> GetAllDoctors(QueryObject query)
    {
        var doctors = await doctorRepository.GetAllAsync(query);
        var res = doctors.Select(x => x.MapShowDoctorDto()).ToList();
        return res;
    }
    
    public async Task<DoctorInfoDto> GetDoctorInfo(string id)
    {
        var doctor = await doctorRepository.GetByIdAsync(id);
        if (doctor is null)
        {
            throw new NullReferenceException("Doctor not found");
        }
        return doctor.MapDoctorInfoDto();
    }
    
    public async Task<Doctor> CreateDoctor(RegistrationDoctorDto dto)
    {
        var doctor = new Doctor
        {
            Id = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            DateOfBirth = dto.DateOfBirth,
            SpecializationId = dto.SpecializationId,
            OfficeId = dto.OfficeId,
            CareerStartYear = dto.CareerStartYear,
            Status = dto.Status
        };
            
        return await doctorRepository.Add(doctor);
    }
    
    public async Task<ShowDoctorDto> UpdateDoctor(string id, UpdateDoctorDto dto)
    {
        var dbDoctor = await doctorRepository.GetByIdAsync(id);
        if (dbDoctor == null)
        { 
            throw new NullReferenceException("Doctor not found");
        }
        
        dbDoctor.OfficeId = dto.OfficeId;
        dbDoctor.FirstName = dto.FirstName;
        dbDoctor.LastName = dto.LastName;
        dbDoctor.MiddleName = dto.MiddleName;
        dbDoctor.SpecializationId = dto.SpecializationId;
        
        var res = await doctorRepository.Update(dbDoctor);

        return res.MapShowDoctorDto();
    }
    
    public async Task<Doctor?> DeleteDoctor(string id)
    {
        var dbDoctor = await doctorRepository.Delete(id);
        if (dbDoctor == null)
        {
            throw new NullReferenceException("Doctor not found");
        }
        return dbDoctor;
    }
}