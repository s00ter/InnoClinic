using InnoClinic.Prof.DataAccess.Entities;
using InnoClinic.Shared.Models;

namespace InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;

public interface IReceptionistRepository
{
    Task<Receptionist> Add(Receptionist patient, 
        CancellationToken cancellationToken = default);
    Task<Receptionist> Update(Receptionist patients, 
        CancellationToken cancellationToken = default);
    Task<Receptionist> Delete(Guid id, 
        CancellationToken cancellationToken = default);
    Task<Receptionist> GetByIdAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task<List<Receptionist>> GetAllAsync(QueryPaginationArguments queryPagination, 
        CancellationToken cancellationToken = default);
}