using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Patient;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Repositories.PatientRepository;
using InnoClinic.Shared.Constants;
using InnoClinic.Shared.Extensions;
using InnoClinic.Shared.IventTypes;
using InnoClinic.Shared.Models;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace InnoClinic.Prof.BusinessLogic.Services.PatientService;

public class PatientService(
    IPatientRepository patientRepository,
    ICurrentUserInfo currentUserInfo,
    IPublishEndpoint publishEndpoint,
    ILogger<PatientService> logger
    ) : IPatientService
{
    public async Task<FrozenSet<ShowPatientResponse>> GetAllPatients(QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        var patients = await patientRepository.GetAllAsync(queryPagination, cancellationToken);
        var res = patients.Select(x => x.MapShowPatientDto()).ToFrozenSet();
        return res;
    }
    
    public async Task<PatientInfoResponse> GetPatientInfo(Guid id, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(id, cancellationToken);
        
        return patient.MapPatientInfoDto();
    }
    
    public async Task<Patient> CreatePatient(RegistrationPatientRequest request, CancellationToken cancellationToken)
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
        var res = await patientRepository.Add(patient, cancellationToken);
        
        logger.LogInformation("Sent message to add {role} role to {userId} user",RoleConstants.Doctor,res.UserId);
        
        await publishEndpoint.Publish<RoleAdded>(new
        {
            UserId = res.UserId,
            Role = RoleConstants.Patient
        }, cancellationToken);

        return res;
    }
    
    public async Task<ShowPatientResponse> UpdatePatient(Guid id, UpdatePatientRequest request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(id, cancellationToken);
        
        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.MiddleName = request.MiddleName;
        patient.IsLinkedToAccount = request.isLinkedToAccount;
        patient.DateOfBirth = request.DateOfBirth;
        
        var res = await patientRepository.Update(patient, cancellationToken);

        return res.MapShowPatientDto();
    }
    
    public async Task<Patient> DeletePatient(Guid id, CancellationToken cancellationToken)
    {
        var res = await patientRepository.Delete(id, cancellationToken);
        
        return res;
    }
}