using InnoClinic.Prof.BusinessLogic.Dto.Receptionist;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;

namespace InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;

public class ReceptionistService(IReceptionistRepository receptionistRepository) : IReceptionistService
{
    public async Task<List<ShowReceptionistDto>> GetAllReceptionists(QueryObject query)
    {
        var patients = await receptionistRepository.GetAllAsync(query);
        var res = patients.Select(x => x.MapShowReceptionistDto()).ToList();
        return res;
    }
    
    public async Task<ReceptionistInfoDto> GetReceptionistInfo(string id)
    {
        var patient = await receptionistRepository.GetByIdAsync(id);
        if (patient is null)
        {
            throw new NullReferenceException("Receptionist not found");
        }
        return patient.MapReceptionistInfoDto();
    }
    
    public async Task<Receptionist> CreateReceptionist(CreateReceptionistDto dto)
    {
        var patient = new Receptionist
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            AccountId = dto.AccountId,
            OfficeId = dto.OfficeId
        };
            
        return await receptionistRepository.Add(patient);
    }
    
    public async Task<ShowReceptionistDto> UpdateReceptionist(string id, UpdateReceptionistDto dto)
    {
        var patient = await receptionistRepository.GetByIdAsync(id);
        if (patient == null)
        { 
            throw new NullReferenceException("Receptionist not found");
        }
        
        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.MiddleName = dto.MiddleName;
        patient.AccountId = dto.AccountId;
        patient.OfficeId = dto.OfficeId;
        
        var res = await receptionistRepository.Update(patient);

        return res.MapShowReceptionistDto();
    }
    
    public async Task<Receptionist?> DeleteReceptionist(string id)
    {
        var res = await receptionistRepository.Delete(id);
        if (res == null)
        {
            throw new NullReferenceException("Receptionist not found");
        }
        return res;
    }
}