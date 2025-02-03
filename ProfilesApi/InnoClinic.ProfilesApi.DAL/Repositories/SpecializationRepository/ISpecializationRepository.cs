using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Prof.DataAccess.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;

public interface ISpecializationRepository
{
    Task<Specialization> Add(Specialization specialization);
    Task<Specialization> Update(Specialization specialization);
    Task UpdateRange(List<Specialization> specializations);
    Task<Specialization?> Delete(string id);
    Task<Specialization?> GetByIdAsync(string id);
    Task<List<Specialization>> GetAllAsync(QueryObject query);
}