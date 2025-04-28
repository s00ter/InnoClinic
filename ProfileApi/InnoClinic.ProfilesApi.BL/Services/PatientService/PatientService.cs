using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Repositories.PatientRepository;
using InnoClinic.Shared.Extensions;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public class PatientService(
    IPatientRepository patientRepository,
    ICurrentUserInfo currentUserInfo
    ) : IPatientService
{
    public async Task<FrozenSet<ShowPatientResponse>> GetAllPatients(QueryPaginationArguments queryPagination)
    {
        var patients = await patientRepository.GetAllAsync(queryPagination);
        var res = patients.Select(x => x.MapShowPatientDto()).ToFrozenSet();
        return res;
    }
    
    public async Task<PatientInfoResponse> GetPatientInfo(Guid id)
    {
        var patient = await patientRepository.GetByIdAsync(id);
        
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
        var patient = await patientRepository.GetByIdAsync(id);
        
        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.MiddleName = request.MiddleName;
        patient.IsLinkedToAccount = request.isLinkedToAccount;
        patient.DateOfBirth = request.DateOfBirth;
        
        var res = await patientRepository.Update(patient);

        return res.MapShowPatientDto();
    }
    
    public async Task<Patient> DeletePatient(Guid id)
    {
        var res = await patientRepository.Delete(id);
        
        return res;
    }
}