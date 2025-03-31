using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.SpecializationService;

public interface ISpecializationService
{
    Task<List<ShowSpecializationResponse>> GetAllSpecializations(QueryObject query);
    Task<SpecializationInfoResponse> GetSpecializationInfo(Guid id);
    Task<Specialization> CreateSpecialization(CreateSpecializationRequest request);
    Task<ShowSpecializationResponse> UpdateSpecialization(Guid id, UpdateSpecializationRequest request);
    Task<Specialization?> DeleteSpecialization(Guid id);
}