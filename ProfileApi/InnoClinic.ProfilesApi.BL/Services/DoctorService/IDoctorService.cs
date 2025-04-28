using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.DoctorService;

public interface IDoctorService
{
    Task<FrozenSet<ShowDoctorResponse>> GetAllDoctors(QueryPaginationArguments queryPagination);
    Task<DoctorInfoResponse> GetDoctorInfo(Guid id);
    Task<Doctor> CreateDoctor(RegistrationDoctorRequest request);
    Task<ShowDoctorResponse> UpdateDoctor(Guid id, UpdateDoctorRequest request);
    Task<Doctor?> DeleteDoctor(Guid id);
}