using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using InnoClinic.Prof.DataAccess.Repositories.PatientRepository;
using InnoClinic.Shared.Extensions;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public class PatientService(
    IPatientRepository patientRepository,
    ICurrentUserInfo currentUserInfo
    ) : IPatientService
{
    public async Task<List<ShowPatientResponse>> GetAllPatients(QueryObject query)
    {
        var patients = await patientRepository.GetAllAsync(query);
        var res = patients.Select(x => x.MapShowPatientDto()).ToList();
        return res;
    }
    
    public async Task<PatientInfoResponse> GetPatientInfo(Guid id)
    {
        var patient = await patientRepository.GetByIdAsync(id) 
                      ?? throw new Exception("Patient not found");
        
        return patient.MapPatientInfoDto();
    }
    
    public async Task<Patient> CreatePatient(RegistrationPatientRequest request)
    {
        var userId = Guid.Parse(currentUserInfo.GetUserId());
        
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            UserId = userId,
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
        var patient = await patientRepository.GetByIdAsync(id) 
                      ?? throw new Exception("Patient not found");
        
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
        var res = await patientRepository.Delete(id) 
                  ?? throw new Exception("Patient not found");
        
        return res;
    }
}