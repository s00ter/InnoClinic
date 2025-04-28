using System.Collections.Frozen;
using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.SpecializationService;

public interface ISpecializationService
{
    Task<FrozenSet<ShowSpecializationResponse>> GetAllSpecializations(QueryPaginationArguments queryPagination);
    Task<SpecializationInfoResponse> GetSpecializationInfo(Guid id);
    Task<Specialization> CreateSpecialization(CreateSpecializationRequest request);
    Task<ShowSpecializationResponse> UpdateSpecialization(Guid id, UpdateSpecializationRequest request);
    Task<Specialization> DeleteSpecialization(Guid id);
}