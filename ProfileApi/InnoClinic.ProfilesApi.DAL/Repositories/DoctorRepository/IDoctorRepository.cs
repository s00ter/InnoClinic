using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.DoctorRepository;

public interface IDoctorRepository
{
    Task<Doctor> Add(Doctor product, 
        CancellationToken cancellationToken = default);
    Task<Doctor> Update(Doctor product, 
        CancellationToken cancellationToken = default);
    Task UpdateRange(List<Doctor> products, 
        CancellationToken cancellationToken = default);
    Task<Doctor> Delete(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Doctor> GetByIdAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task<List<Doctor>> GetAllAsync(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
}