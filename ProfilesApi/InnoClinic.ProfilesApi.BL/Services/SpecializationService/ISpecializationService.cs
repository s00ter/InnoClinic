using InnoClinic.Prof.BusinessLogic.Dto.Specialization;
using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.BusinessLogic.Services.SpecializationService;

public interface ISpecializationService
{
    Task<List<ShowSpecializationDto>> GetAllSpecializations(QueryObject query);
    Task<SpecializationInfoDto> GetSpecializationInfo(string id);
    Task<Specialization> CreateSpecialization(CreateSpecializationDto dto);
    Task<ShowSpecializationDto> UpdateSpecialization(string id, UpdateSpecializationDto dto);
    Task<Specialization?> DeleteSpecialization(string id);
}