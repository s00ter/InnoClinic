using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public interface IPatientService
{
    Task<FrozenSet<ShowPatientResponse>> GetAllPatients(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
    Task<PatientInfoResponse> GetPatientInfo(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Patient> CreatePatient(RegistrationPatientRequest request, 
        CancellationToken cancellationToken = default);
    Task<ShowPatientResponse> UpdatePatient(Guid id, UpdatePatientRequest request, 
        CancellationToken cancellationToken = default);
    Task<Patient> DeletePatient(Guid id, 
        CancellationToken cancellationToken = default);
}