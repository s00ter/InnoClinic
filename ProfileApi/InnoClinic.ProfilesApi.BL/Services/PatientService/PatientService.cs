using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using InnoClinic.Prof.DataAccess.Repositories.PatientRepository;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<List<ShowPatientResponse>> GetAllPatients(QueryObject query)
    {
        var patients = await patientRepository.GetAllAsync(query);
        var res = patients.Select(x => x.MapShowPatientDto()).ToList();
        return res;
    }
    
    public async Task<PatientInfoResponse> GetPatientInfo(Guid id)
    {
        var patient = await patientRepository.GetByIdAsync(id);
        if (patient is null)
        {
            throw new NullReferenceException("Doctor not found");
        }
        return patient.MapPatientInfoDto();
    }
    
    public async Task<Patient> CreatePatient(RegistrationPatientRequest request)
    {
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            IsLinkedToAccount = request.isLinkedToAccount,
            DateOfBirth = request.DateOfBirth
        };
            
        return await patientRepository.Add(patient);
    }
    
    public async Task<ShowPatientResponse> UpdatePatient(Guid id, UpdatePatientRequest request)
    {
        var patient = await patientRepository.GetByIdAsync(id);
        if (patient == null)
        { 
            throw new NullReferenceException("Patient not found");
        }
        
        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.MiddleName = request.MiddleName;
        patient.IsLinkedToAccount = request.isLinkedToAccount;
        patient.DateOfBirth = request.DateOfBirth;
        
        var res = await patientRepository.Update(patient);

        return res.MapShowPatientDto();
    }
    
    public async Task<Patient?> DeletePatient(Guid id)
    {
        var res = await patientRepository.Delete(id);
        if (res == null)
        {
            throw new NullReferenceException("Patient not found");
        }
        return res;
    }
}