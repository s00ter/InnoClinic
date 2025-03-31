using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public interface IPatientService
{
    Task<List<ShowPatientResponse>> GetAllPatients(QueryObject query);
    Task<PatientInfoResponse> GetPatientInfo(Guid id);
    Task<Patient> CreatePatient(RegistrationPatientRequest request);
    Task<ShowPatientResponse> UpdatePatient(Guid id, UpdatePatientRequest request);
    Task<Patient?> DeletePatient(Guid id);
}