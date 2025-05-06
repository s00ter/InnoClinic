using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;

public interface ISpecializationRepository
{
    Task<Specialization> Add(Specialization specialization, 
        CancellationToken cancellationToken = default);
    Task<Specialization> Update(Specialization specialization, 
        CancellationToken cancellationToken = default);
    Task Delete(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Specialization> GetByIdAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task<List<Specialization>> GetAllAsync(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
}