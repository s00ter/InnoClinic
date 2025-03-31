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
    public async Task<List<ShowSpecializationResponse>> GetAllSpecializations(QueryObject query)
    {
        var specializations = await specializationRepository.GetAllAsync(query);
        var res = specializations.Select(x => x.MapShowSpecializationDto()).ToList();
        return res;
    }
    
    public async Task<SpecializationInfoResponse> GetSpecializationInfo(Guid id)
    {
        var specialization = await specializationRepository.GetByIdAsync(id) 
                             ?? throw new Exception("Specialization not found");
        
        return specialization.MapSpecializationInfoDto();
    }
    
    public async Task<Specialization> CreateSpecialization(CreateSpecializationRequest request)
    {
        var specialization = new Specialization
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            IsActive = request.IsActive,
        };
            
        return await specializationRepository.Add(specialization);
    }
    
    public async Task<ShowSpecializationResponse> UpdateSpecialization(Guid id, UpdateSpecializationRequest request)
    {
        var specialization = await specializationRepository.GetByIdAsync(id) 
                             ?? throw new Exception("Specialization not found");
        
        specialization.Name = request.Name;
        specialization.IsActive = request.IsActive;
        
        var res = await specializationRepository.Update(specialization);

        return res.MapShowSpecializationDto();
    }
    
    public async Task<Specialization?> DeleteSpecialization(Guid id)
    {
        var res = await specializationRepository.Delete(id) 
                  ?? throw new Exception("Specialization not found");
        
        return res;
    }
}