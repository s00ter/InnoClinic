using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;

public interface ISpecializationRepository
{
    Task<Specialization> Add(Specialization specialization);
    Task<Specialization> Update(Specialization specialization);
    Task UpdateRange(List<Specialization> specializations);
    Task<Specialization> Delete(Guid id);
    Task<Specialization> GetByIdAsync(Guid id);
    Task<List<Specialization>> GetAllAsync(QueryPaginationArguments queryPagination);
}