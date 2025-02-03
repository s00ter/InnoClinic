using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using InnoClinic.Prof.DataAccess.Repositories.PatientRepository;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<List<ShowPatientDto>> GetAllPatients(QueryObject query)
    {
        var patients = await patientRepository.GetAllAsync(query);
        var res = patients.Select(x => x.MapShowPatientDto()).ToList();
        return res;
    }
    
    public async Task<PatientInfoDto> GetPatientInfo(string id)
    {
        var patient = await patientRepository.GetByIdAsync(id);
        if (patient is null)
        {
            throw new NullReferenceException("Doctor not found");
        }
        return patient.MapPatientInfoDto();
    }
    
    public async Task<Patient> CreatePatient(RegistrationPatientDto dto)
    {
        var patient = new Patient
        {
            Id = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            IsLinkedToAccount = dto.isLinkedToAccount,
            DateOfBirth = dto.DateOfBirth
        };
            
        return await patientRepository.Add(patient);
    }
    
    public async Task<ShowPatientDto> UpdatePatient(string id, UpdatePatientDto dto)
    {
        var patient = await patientRepository.GetByIdAsync(id);
        if (patient == null)
        { 
            throw new NullReferenceException("Patient not found");
        }
        
        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.MiddleName = dto.MiddleName;
        patient.IsLinkedToAccount = dto.isLinkedToAccount;
        patient.DateOfBirth = dto.DateOfBirth;
        
        var res = await patientRepository.Update(patient);

        return res.MapShowPatientDto();
    }
    
    public async Task<Patient?> DeletePatient(string id)
    {
        var res = await patientRepository.Delete(id);
        if (res == null)
        {
            throw new NullReferenceException("Patient not found");
        }
        return res;
    }
}