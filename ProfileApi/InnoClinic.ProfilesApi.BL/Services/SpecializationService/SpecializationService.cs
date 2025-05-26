using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.BusinessLogic.Mappers;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.SpecializationService;

public class SpecializationService(
    ISpecializationRepository specializationRepository
    ) : ISpecializationService
{
    public async Task<FrozenSet<ShowSpecializationResponse>> GetAllSpecializations(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken)
    {
        var specializations = await specializationRepository.GetAllAsync(queryPagination, cancellationToken);
        var res = specializations.Select(x => x.MapShowSpecializationDto()).ToFrozenSet();
        return res;
    }
    
    public async Task<SpecializationInfoResponse> GetSpecializationInfo(Guid id, 
        CancellationToken cancellationToken)
    {
        var specialization = await specializationRepository.GetByIdAsync(id, cancellationToken);
        
        return specialization.MapSpecializationInfoDto();
    }
    
    public async Task<Specialization> CreateSpecialization(CreateSpecializationRequest request, 
        CancellationToken cancellationToken)
    {
        var specialization = new Specialization
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            IsActive = request.IsActive,
        };
            
        return await specializationRepository.Add(specialization, cancellationToken);
    }
    
    public async Task<ShowSpecializationResponse> UpdateSpecialization(Guid id, UpdateSpecializationRequest request, 
        CancellationToken cancellationToken)
    {
        var specialization = await specializationRepository.GetByIdAsync(id, cancellationToken);
        
        specialization.Name = request.Name;
        specialization.IsActive = request.IsActive;
        
        var res = await specializationRepository.Update(specialization, cancellationToken);

        return res.MapShowSpecializationDto();
    }
    
    public async Task DeleteSpecialization(Guid id, 
        CancellationToken cancellationToken)
    {
        await specializationRepository.Delete(id, cancellationToken);
    }
}