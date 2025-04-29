using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.SpecializationService;

public interface ISpecializationService
{
    Task<FrozenSet<ShowSpecializationResponse>> GetAllSpecializations(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
    Task<SpecializationInfoResponse> GetSpecializationInfo(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Specialization> CreateSpecialization(CreateSpecializationRequest request, 
        CancellationToken cancellationToken = default);
    Task<ShowSpecializationResponse> UpdateSpecialization(Guid id, UpdateSpecializationRequest request, 
        CancellationToken cancellationToken = default);
    Task DeleteSpecialization(Guid id, 
        CancellationToken cancellationToken = default);
}