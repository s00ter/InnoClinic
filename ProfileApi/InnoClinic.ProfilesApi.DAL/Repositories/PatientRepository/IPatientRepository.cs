using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.PatientRepository;

public interface IPatientRepository
{
    Task<Patient> Add(Patient patient, 
        CancellationToken cancellationToken = default);
    Task<Patient> Update(Patient patients, 
        CancellationToken cancellationToken = default);
    Task UpdateRange(List<Patient> patients, 
        CancellationToken cancellationToken = default);
    Task<Patient> Delete(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Patient> GetByIdAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task<List<Patient>> GetAllAsync(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
}