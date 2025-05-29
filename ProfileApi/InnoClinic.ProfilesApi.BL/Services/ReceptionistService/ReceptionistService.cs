using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;
using InnoClinic.Shared.Constants;
using InnoClinic.Shared.Extensions;
using InnoClinic.Shared.IventTypes;
using InnoClinic.Shared.Models;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;

public class ReceptionistService(
    IReceptionistRepository receptionistRepository,
    ICurrentUserInfo currentUserInfo,
    IPublishEndpoint publishEndpoint,
    ILogger<ReceptionistService> logger
    ) : IReceptionistService
{
    public async Task<FrozenSet<ShowReceptionistResponse>> GetAllReceptionists(QueryPaginationArguments queryPagination, CancellationToken cancellationToken)
    {
        var patients = await receptionistRepository.GetAllAsync(queryPagination, cancellationToken);
        var res = patients.Select(x => x.MapShowReceptionistDto()).ToFrozenSet();
        return res;
    }
    
    public async Task<ReceptionistInfoResponse> GetReceptionistInfo(Guid id, CancellationToken cancellationToken)
    {
        var patient = await receptionistRepository.GetByIdAsync(id, cancellationToken);
        
        return patient.MapReceptionistInfoDto();
    }
    
    public async Task<Receptionist> CreateReceptionist(RegistrationReceptionistRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(currentUserInfo.GetUserId());
        
        var patient = new Receptionist
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            AccountId = userId,
            OfficeId = request.OfficeId
        };
        var res = await receptionistRepository.Add(patient, cancellationToken);
        
        logger.LogInformation("Sent message to add {role} role to {userId} user",RoleConstants.Doctor,res.AccountId);
        
        await publishEndpoint.Publish<RoleAdded>(new
        {
            UserId = res.AccountId,
            Role = RoleConstants.Receptionist
        }, cancellationToken);

        return res;
    }
    
    public async Task<ShowReceptionistResponse> UpdateReceptionist(Guid id, UpdateReceptionistRequest request, CancellationToken cancellationToken)
    {
        var patient = await receptionistRepository.GetByIdAsync(id, cancellationToken);
        
        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.MiddleName = request.MiddleName;
        patient.OfficeId = request.OfficeId;
        
        var res = await receptionistRepository.Update(patient, cancellationToken);

        return res.MapShowReceptionistDto();
    }
    
    public async Task<Receptionist?> DeleteReceptionist(Guid id, CancellationToken cancellationToken)
    {
        var res = await receptionistRepository.Delete(id, cancellationToken);
        
        return res;
    }
}