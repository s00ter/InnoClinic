using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.DoctorService;

public interface IDoctorService
{
    Task<List<ShowDoctorResponse>> GetAllDoctors(QueryObject query);
    Task<DoctorInfoResponse> GetDoctorInfo(Guid id);
    Task<Doctor> CreateDoctor(RegistrationDoctorRequest request);
    Task<ShowDoctorResponse> UpdateDoctor(Guid id, UpdateDoctorRequest request);
    Task<Doctor?> DeleteDoctor(Guid id);
}