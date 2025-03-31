using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;
using InnoClinic.Shared.Extensions;

namespace InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;

public class ReceptionistService(
    IReceptionistRepository receptionistRepository,
    ICurrentUserInfo currentUserInfo
    ) : IReceptionistService
{
    public async Task<List<ShowReceptionistResponse>> GetAllReceptionists(QueryObject query)
    {
        var patients = await receptionistRepository.GetAllAsync(query);
        var res = patients.Select(x => x.MapShowReceptionistDto()).ToList();
        return res;
    }
    
    public async Task<ReceptionistInfoResponse> GetReceptionistInfo(Guid id)
    {
        var patient = await receptionistRepository.GetByIdAsync(id) 
                      ?? throw new Exception("Receptionist not found");
        
        return patient.MapReceptionistInfoDto();
    }
    
    public async Task<Receptionist> CreateReceptionist(RegistrationReceptionistRequest request)
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
            
        return await receptionistRepository.Add(patient);
    }
    
    public async Task<ShowReceptionistResponse> UpdateReceptionist(Guid id, UpdateReceptionistRequest request)
    {
        var patient = await receptionistRepository.GetByIdAsync(id) 
                      ?? throw new Exception("Receptionist not found");
        
        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.MiddleName = request.MiddleName;
        patient.OfficeId = request.OfficeId;
        
        var res = await receptionistRepository.Update(patient);

        return res.MapShowReceptionistDto();
    }
    
    public async Task<Receptionist?> DeleteReceptionist(Guid id)
    {
        var res = await receptionistRepository.Delete(id) 
                  ?? throw new NullReferenceException("Receptionist not found");
        
        return res;
    }
}