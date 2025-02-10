using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;
using InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;

namespace InnoClinic.Prof.BusinessLogic.Services.SpecializationService;

public class SpecializationService(
    ISpecializationRepository specializationRepository
    ) : ISpecializationService
{
    public async Task<List<ShowSpecializationDto>> GetAllSpecializations(QueryObject query)
    {
        var specializations = await specializationRepository.GetAllAsync(query);
        var res = specializations.Select(x => x.MapShowSpecializationDto()).ToList();
        return res;
    }
    
    public async Task<SpecializationInfoDto> GetSpecializationInfo(string id)
    {
        var specialization = await specializationRepository.GetByIdAsync(id);
        if (specialization is null)
        {
            throw new NullReferenceException("Specialization not found");
        }
        return specialization.MapSpecializationInfoDto();
    }
    
    public async Task<Specialization> CreateSpecialization(CreateSpecializationDto dto)
    {
        var specialization = new Specialization
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            IsActive = dto.IsActive,
        };
            
        return await specializationRepository.Add(specialization);
    }
    
    public async Task<ShowSpecializationDto> UpdateSpecialization(string id, UpdateSpecializationDto dto)
    {
        var specialization = await specializationRepository.GetByIdAsync(id);
        if (specialization == null)
        { 
            throw new NullReferenceException("Specialization not found");
        }
        
        specialization.Name = dto.Name;
        specialization.IsActive = dto.IsActive;
        
        var res = await specializationRepository.Update(specialization);

        return res.MapShowSpecializationDto();
    }
    
    public async Task<Specialization?> DeleteSpecialization(string id)
    {
        var res = await specializationRepository.Delete(id);
        if (res == null)
        {
            throw new NullReferenceException("Specialization not found");
        }
        return res;
    }
}