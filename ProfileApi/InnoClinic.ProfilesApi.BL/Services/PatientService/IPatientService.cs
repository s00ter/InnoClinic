using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public interface IPatientService
{
    Task<FrozenSet<ShowPatientResponse>> GetAllPatients(QueryPaginationArguments queryPagination);
    Task<PatientInfoResponse> GetPatientInfo(Guid id);
    Task<Patient> CreatePatient(RegistrationPatientRequest request);
    Task<ShowPatientResponse> UpdatePatient(Guid id, UpdatePatientRequest request);
    Task<Patient> DeletePatient(Guid id);
}