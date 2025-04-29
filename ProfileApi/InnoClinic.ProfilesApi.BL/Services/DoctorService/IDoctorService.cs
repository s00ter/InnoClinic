using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Doctor;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.DoctorService;

public interface IDoctorService
{
    Task<FrozenSet<ShowDoctorResponse>> GetAllDoctors(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
    Task<DoctorInfoResponse> GetDoctorInfo(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Doctor> CreateDoctor(RegistrationDoctorRequest request, 
        CancellationToken cancellationToken = default);
    Task<ShowDoctorResponse> UpdateDoctor(Guid id, UpdateDoctorRequest request, 
        CancellationToken cancellationToken = default);
    Task<Doctor> DeleteDoctor(Guid id, 
        CancellationToken cancellationToken = default);
}