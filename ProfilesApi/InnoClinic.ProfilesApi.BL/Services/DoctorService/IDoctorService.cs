using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.DoctorService;

public interface IDoctorService
{
    Task<List<ShowDoctorDto>> GetAllDoctors(QueryObject query);
    Task<DoctorInfoDto> GetDoctorInfo(string id);
    Task<Doctor> CreateDoctor(RegistrationDoctorDto dto);
    Task<ShowDoctorDto> UpdateDoctor(string id, UpdateDoctorDto dto);
    Task<Doctor?> DeleteDoctor(string id);
}