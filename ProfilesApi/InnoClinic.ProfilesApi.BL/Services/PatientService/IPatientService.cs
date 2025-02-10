using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public interface IPatientService
{
    Task<List<ShowPatientDto>> GetAllPatients(QueryObject query);
    Task<PatientInfoDto> GetPatientInfo(string id);
    Task<Patient> CreatePatient(RegistrationPatientDto dto);
    Task<ShowPatientDto> UpdatePatient(string id, UpdatePatientDto dto);
    Task<Patient?> DeletePatient(string id);
}